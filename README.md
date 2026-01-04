# Cirreum Persistence NoSql Library

[![NuGet Version](https://img.shields.io/nuget/v/Cirreum.Persistence.NoSql.svg?style=flat-square)](https://www.nuget.org/packages/Cirreum.Persistence.NoSql/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Cirreum.Persistence.NoSql.svg?style=flat-square)](https://www.nuget.org/packages/Cirreum.Persistence.NoSql/)
[![GitHub Release](https://img.shields.io/github/v/release/cirreum/Cirreum.Persistence.NoSql?style=flat-square)](https://github.com/cirreum/Cirreum.Persistence.NoSql/releases)

A comprehensive .NET persistence NoSql based abstraction library that provides type-safe, feature-rich repository patterns for data access operations without external dependencies. This library offers a clean separation between your domain logic and persistence concerns through well-defined interfaces and base implementations

## Overview

The Cirreum Persistence Library contains abstractions and contracts for implementing persistence repositories with support for:

- **Entity Management**: Base entity types with built-in auditing, soft deletes, ETags, and TTL
- **Repository Patterns**: Read-only, write-only, batch, and full repository interfaces
- **Advanced Querying**: Expression-based queries, pagination, and async enumeration
- **Partial Updates**: Patch operations with expression and path-based targeting
- **Soft Deletes**: Built-in soft delete and restore capabilities
- **Auditing**: Automatic tracking of creation, modification, and deletion metadata
- **Time Zones**: IANA timezone support for audit fields
- **Concurrency Control**: ETag-based optimistic concurrency
- **Time-to-Live**: Automatic entity expiration support

## Core Interfaces

### Entity Interfaces

- **`IEntity`**: Base entity contract with Id, EntityType, and PartitionKey
- **`IAuditableEntity`**: Adds creation and modification tracking with timezone support
- **`IDeletableEntity`**: Enables soft delete capabilities
- **`IRestorableEntity`**: Extends soft delete with restoration tracking
- **`IEtagEntity`**: Provides optimistic concurrency control
- **`ITimeToLiveEntity`**: Automatic entity expiration

### Repository Interfaces

- **`IReadOnlyRepository<TEntity>`**: Query, retrieval, counting, and pagination operations
- **`IWriteOnlyRepository<TEntity>`**: Create, update, delete, and restore operations
- **`IBatchRepository<TEntity>`**: Batch operations for multiple entities
- **`IRepository<TEntity>`**: Combined interface extending all repository types

## Entity Base Classes

### BaseEntity

```csharp
public abstract record BaseEntity : IEntity
{
	public string Id { get; set; } = Guid.NewGuid().ToString();
	public string EntityType { get; init; }
	string IEntity.PartitionKey => GetPartitionKeyValue();
}
```

### Entity (Full-Featured)

```csharp
public abstract record Entity : BaseEntity, IEtagEntity, ITimeToLiveEntity, IAuditableEntity
{
	public string Etag { get; init; }
	public TimeSpan? TimeToLive { get; set; }
	public DateTimeOffset? CreatedOn { get; set; }
	public string? CreatedBy { get; set; }
	public string? CreatedInTimeZone { get; set; }
	public DateTimeOffset ModifiedOn { get; }
	public string ModifiedBy { get; set; }
	public string? ModifiedInTimeZone { get; set; }
}
```

### SoftDeleteEntity

```csharp
public abstract record SoftDeleteEntity : Entity, IRestorableEntity
{
	public string DeletedBy { get; set; }
	public DateTimeOffset? DeletedOn { get; set; }
	public string? DeletedInTimeZone { get; set; }
	public bool IsDeleted { get; set; }
	public int RestoreCount { get; set; }
}
```

## Key Features

### 1. Comprehensive Querying

```csharp
// Expression-based queries
var results = await repository.QueryAsync(x => x.Name.Contains("test"));

// Pagination with continuation tokens
var page = await repository.PageContinuationAsync(
	predicate: x => x.IsActive, 
	pageSize: 20, 
	continuationToken: null);

// Offset-based pagination
var offsetPage = await repository.PageOffsetAsync(
	predicate: null, 
	pageNumber: 1, 
	pageSize: 10, 
	includeTotalCount: true);

// Async enumeration for large datasets
await foreach (var entity in repository.SequenceQueryAsync(x => x.Category == "Important"))
{
	// Process entity
}
```

### 2. Partial Updates with Patch Operations

```csharp
// Expression-based patch operations
await repository.UpdatePartialAsync(entityId, builder => builder
	.Set(x => x.Name, "New Name")
	.Increment(x => x.ViewCount, 1)
	.Add(x => x.Tags, "new-tag")
	.Remove(x => x.ObsoleteField));

// Path-based operations for interface properties
await repository.UpdatePartialAsync(entityId, builder => builder
	.SetByPath("status", "Active")
	.IncrementByPath("score", 10));
```

### 3. Soft Delete and Restore

```csharp
// Soft delete
await repository.DeleteAsync(entityId, softDelete: true);

// Restore with tracking
var (wasRestored, entity) = await repository.RestoreAsync(entityId);

// Batch restore
var results = await repository.RestoreAsBatchAsync(entities);
```

### 4. Batch Operations

```csharp
// All entities must share the same partition
await repository.CreateAsBatchAsync(entities);
await repository.UpdateAsBatchAsync(entities);
await repository.DeleteAsBatchAsync(entities, softDelete: true);
```

### 5. Advanced Query Options

```csharp
// Include soft-deleted entities
var allEntities = await repository.GetAllAsync(includeDeleted: true);

// Check existence
var exists = await repository.ExistsAsync(x => x.Email == "user@example.com");

// Count with filters
var count = await repository.CountAsync(x => x.IsActive && !x.IsDeleted);
```

## Attributes for Persistence Configuration

### Container Specification

```csharp
[Container("my-container")]
public record MyEntity : Entity;
```

### Partition Key Configuration

```csharp
[PartitionKeyPath("/tenantId")]
public record TenantEntity : Entity {

	public string TenantId { get; set; }

	protected override string GetPartitionKeyValue() => this.TenantId;

}
```

### Unique Key Constraints

```csharp
public record UserEntity : Entity {

	[UniqueKey("email-key", "/email")]
	public string Email { get; set; }
	
	[UniqueKey("username-key", "/username")]
	public string Username { get; set; }

}
```

## Pagination Support

The library provides two pagination approaches:

### Continuation Token Paging (Recommended)

- More efficient for large datasets
- Lower resource usage
- Returns `IContinuationPage<T>` with continuation tokens

### Offset Paging

- Traditional page number-based paging
- Higher resource usage but familiar UX
- Returns `IOffSetPage<T>` with page numbers and totals

## Timezone Support

All audit fields support IANA timezone identifiers:

```csharp
entity.CreatedInTimeZone = "America/Phoenix";
entity.ModifiedInTimeZone = "Europe/London";
entity.DeletedInTimeZone = "Asia/Tokyo";
```

## Thread Safety and Concurrency

- ETag-based optimistic concurrency control
- Concurrency tokens for partial updates
- Thread-safe read operations
- Batch operations require entities in same partition

## Best Practices

1. **Inherit from appropriate base classes**: Use `Entity` for full features, `BaseEntity` for minimal requirements
2. **Implement partition strategy**: Override `GetPartitionKeyValue()` for custom partitioning
3. **Use continuation paging**: Prefer continuation tokens over offset paging for performance
4. **Batch same-partition entities**: Ensure batch operations only include entities from the same partition
5. **Handle soft deletes**: Use `includeDeleted` parameter appropriately in queries
6. **Leverage partial updates**: Use patch operations for efficient field-level updates

## Dependencies

This library has **zero external dependencies** and works with:

- System.Text.Json for serialization attributes
- System.Linq.Expressions for query building

## Integration

This abstraction layer is designed to be implemented by concrete persistence providers such as:

- Azure Cosmos DB
- MongoDB
- Entity Framework Core
- Any document or relational database

The clean separation ensures your domain logic remains persistence-agnostic while providing rich functionality through the unified interface.

## Contributing

This package is part of the Cirreum ecosystem. Follow the established patterns when contributing new features or provider implementations.


**Cirreum Foundation Framework**  
*Layered simplicity for modern .NET*
