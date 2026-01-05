namespace Cirreum.Persistence;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// A repository interface for Read Only operations.
/// </summary>
/// <typeparam name="TEntity">The <see cref="IEntity"/> implementation class type.</typeparam>
public interface IReadOnlyRepository<TEntity> where TEntity : IEntity {

	#region Get Methods

	/// <summary>
	/// Gets the <see cref="IEntity"/> that corresponds to the given <paramref name="id"/>.
	/// </summary>
	/// <param name="id">The string identifier.</param>
	/// <param name="includeDeleted">
	/// If <see langword="true"/>, includes entities that are marked as deleted; otherwise, excludes them.
	/// </param>
	/// <param name="cancellationToken">The cancellation token for asynchronous operations.</param>
	/// <returns>
	/// A <see cref="ValueTask{TEntity}"/> representing the <see cref="IEntity"/> instance as a <typeparamref name="TEntity"/>.
	/// </returns>
	ValueTask<TEntity> GetAsync(
		string id,
		bool includeDeleted,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets the <see cref="IEntity"/> that corresponds to the given <paramref name="id"/>.
	/// This overload defaults to not including deleted entities.
	/// </summary>
	/// <param name="id">The string identifier.</param>
	/// <param name="cancellationToken">The cancellation token for asynchronous operations.</param>
	/// <returns>
	/// A <see cref="ValueTask{TEntity}"/> representing the <see cref="IEntity"/> instance as a <typeparamref name="TEntity"/>.
	/// </returns>
	public ValueTask<TEntity> GetAsync(
		string id,
		CancellationToken cancellationToken = default) {
		return this.GetAsync(id, false, cancellationToken);
	}

	/// <summary>
	/// Gets many <typeparamref name="TEntity"/> instances corresponding to the provided list of <paramref name="ids"/>.
	/// </summary>
	/// <param name="ids">The list of identifiers.</param>
	/// <param name="includeDeleted">
	/// If <see langword="true"/>, includes entities marked as deleted; otherwise, excludes them.
	/// </param>
	/// <param name="cancellationToken">The cancellation token for asynchronous operations.</param>
	/// <returns>An <see cref="IReadOnlyList{TEntity}"/> of matching <typeparamref name="TEntity"/> instances.</returns>
	ValueTask<IReadOnlyList<TEntity>> GetManyAsync(
		IEnumerable<string> ids,
		bool includeDeleted,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets many <typeparamref name="TEntity"/> instances corresponding to the provided list of <paramref name="ids"/>.
	/// This overload defaults to not including deleted entities.
	/// </summary>
	/// <param name="ids">The list of identifiers.</param>
	/// <param name="cancellationToken">The cancellation token for asynchronous operations.</param>
	/// <returns>An <see cref="IReadOnlyList{TEntity}"/> of matching <typeparamref name="TEntity"/> instances.</returns>
	public ValueTask<IReadOnlyList<TEntity>> GetManyAsync(
		IEnumerable<string> ids,
		CancellationToken cancellationToken = default) {
		return this.GetManyAsync(ids, false, cancellationToken);
	}

	/// <summary>
	/// Gets an <see cref="IReadOnlyList{TEntity}"/> collection of all <typeparamref name="TEntity"/> items.
	/// </summary>
	/// <param name="includeDeleted">
	/// If <see langword="true"/>, includes items marked as deleted; otherwise, excludes them.
	/// </param>
	/// <param name="cancellationToken">The cancellation token for asynchronous operations.</param>
	/// <returns>A collection of all <typeparamref name="TEntity"/> instances.</returns>
	ValueTask<IReadOnlyList<TEntity>> GetAllAsync(
		bool includeDeleted,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets an <see cref="IReadOnlyList{TEntity}"/> collection of all <typeparamref name="TEntity"/> items.
	/// This overload defaults to not including deleted entities.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token for asynchronous operations.</param>
	/// <returns>A collection of all <typeparamref name="TEntity"/> instances.</returns>
	public ValueTask<IReadOnlyList<TEntity>> GetAllAsync(
		CancellationToken cancellationToken = default) {
		return this.GetAllAsync(false, cancellationToken);
	}

	#endregion

	#region FirstOrNull Methods

	/// <summary>
	/// Gets the first <typeparamref name="TEntity"/> that matches the specified predicate, or <see langword="null"/> if no match is found.
	/// </summary>
	/// <param name="predicate">An expression to filter the results.</param>
	/// <param name="includeDeleted">
	/// If <see langword="true"/>, includes items marked as deleted; otherwise, excludes them.
	/// </param>
	/// <param name="cancellationToken">The cancellation token for asynchronous operations.</param>
	/// <returns>The first matching <typeparamref name="TEntity"/>, or <see langword="null"/> if none is found.</returns>
	ValueTask<TEntity?> FirstOrNullAsync(
		Expression<Func<TEntity, bool>> predicate,
		bool includeDeleted,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets the first <typeparamref name="TEntity"/> that matches the specified predicate, or <see langword="null"/> if no match is found.
	/// This overload defaults to not including deleted entities.
	/// </summary>
	/// <param name="predicate">An expression to filter the results.</param>
	/// <param name="cancellationToken">The cancellation token for asynchronous operations.</param>
	/// <returns>The first matching <typeparamref name="TEntity"/>, or <see langword="null"/> if none is found.</returns>
	public ValueTask<TEntity?> FirstOrNullAsync(
		Expression<Func<TEntity, bool>> predicate,
		CancellationToken cancellationToken = default) {
		return this.FirstOrNullAsync(predicate, false, cancellationToken);
	}

	#endregion

	#region Query Methods

	/// <summary>
	/// Gets a collection of <typeparamref name="TEntity"/> entities that match the specified predicate.
	/// </summary>
	/// <remarks>
	/// Entities whose type does not match <typeparamref name="TEntity"/> are excluded.
	/// </remarks>
	/// <param name="predicate">An expression to filter the results.</param>
	/// <param name="includeDeleted">
	/// If <see langword="true"/>, includes entities marked as deleted; otherwise, excludes them.
	/// </param>
	/// <param name="cancellationToken">The cancellation token for asynchronous operations.</param>
	/// <returns>A collection of matching <typeparamref name="TEntity"/> instances.</returns>
	ValueTask<IReadOnlyList<TEntity>> QueryAsync(
		Expression<Func<TEntity, bool>> predicate,
		bool includeDeleted,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets a collection of <typeparamref name="TEntity"/> items that match the specified predicate.
	/// This overload defaults to not including deleted entities.
	/// </summary>
	/// <param name="predicate">An expression to filter the results.</param>
	/// <param name="cancellationToken">The cancellation token for asynchronous operations.</param>
	/// <returns>A collection of matching <typeparamref name="TEntity"/> instances.</returns>
	public ValueTask<IReadOnlyList<TEntity>> QueryAsync(
		Expression<Func<TEntity, bool>> predicate,
		CancellationToken cancellationToken = default) {
		return this.QueryAsync(predicate, false, cancellationToken);
	}

	/// <summary>
	/// Gets a collection of <typeparamref name="TEntity"/> items by executing the specified query.
	/// </summary>
	/// <param name="query">The query.</param>
	/// <param name="cancellationToken">The cancellation token for asynchronous operations.</param>
	/// <returns>A collection of <typeparamref name="TEntity"/> instances returned by the query.</returns>
	ValueTask<IReadOnlyList<TEntity>> QueryAsync(
		string query,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets a collection of <typeparamref name="TEntity"/> items by executing a parameterized query.
	/// </summary>
	/// <param name="parameterizedQuery">The query template with placeholders for parameters.</param>
	/// <param name="parameters">A collection of key/value pairs representing query parameters.</param>
	/// <param name="cancellationToken">The cancellation token for asynchronous operations.</param>
	/// <returns>A collection of <typeparamref name="TEntity"/> instances returned by the query.</returns>
	ValueTask<IReadOnlyList<TEntity>> QueryAsync(
		string parameterizedQuery,
		IEnumerable<KeyValuePair<string, string>> parameters,
		CancellationToken cancellationToken = default);

	#endregion

	#region Sequence Query Methods

	/// <summary>
	/// Gets an asynchronous sequence of <typeparamref name="TEntity"/> entities that match the specified predicate.
	/// </summary>
	/// <param name="predicate">An expression to filter the results.</param>
	/// <param name="includeDeleted">
	/// If <see langword="true"/>, includes entities marked as deleted; otherwise, excludes them.
	/// </param>
	/// <param name="maxResults">Optional maximum number of entities to return; if <see langword="null"/>, returns all matches.</param>
	/// <param name="cancellationToken">The cancellation token for asynchronous operations.</param>
	/// <returns>An <see cref="IAsyncEnumerable{TEntity}"/> representing the matching sequence.</returns>
	IAsyncEnumerable<TEntity> SequenceQueryAsync(
		Expression<Func<TEntity, bool>> predicate,
		bool includeDeleted,
		int? maxResults = null,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets an asynchronous sequence of <typeparamref name="TEntity"/> items that match the specified predicate.
	/// This overload defaults to not including deleted entities.
	/// </summary>
	/// <param name="predicate">An expression to filter the results.</param>
	/// <param name="maxResults">Optional maximum number of items to return; if <see langword="null"/>, returns all matches.</param>
	/// <param name="cancellationToken">The cancellation token for asynchronous operations.</param>
	/// <returns>An <see cref="IAsyncEnumerable{TEntity}"/> representing the matching sequence.</returns>
	public IAsyncEnumerable<TEntity> SequenceQueryAsync(
		Expression<Func<TEntity, bool>> predicate,
		int? maxResults = null,
		CancellationToken cancellationToken = default) {
		return this.SequenceQueryAsync(predicate, false, maxResults, cancellationToken);
	}

	#endregion

	#region Exists Methods

	/// <summary>
	/// Determines whether an entity with the specified <paramref name="id"/> exists.
	/// </summary>
	/// <param name="id">The string identifier.</param>
	/// <param name="includeDeleted">
	/// If <see langword="true"/>, considers entities marked as deleted; otherwise, excludes them.
	/// </param>
	/// <param name="cancellationToken">The cancellation token for asynchronous operations.</param>
	/// <returns><see langword="true"/> if the entity exists; otherwise, <see langword="false"/>.</returns>
	ValueTask<bool> ExistsAsync(
		string id,
		bool includeDeleted,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Determines whether an entity with the specified <paramref name="id"/> exists.
	/// This overload defaults to not including deleted entities.
	/// </summary>
	/// <param name="id">The string identifier.</param>
	/// <param name="cancellationToken">The cancellation token for asynchronous operations.</param>
	/// <returns><see langword="true"/> if the entity exists; otherwise, <see langword="false"/>.</returns>
	public ValueTask<bool> ExistsAsync(
		string id,
		CancellationToken cancellationToken = default) {
		return this.ExistsAsync(id, false, cancellationToken);
	}

	/// <summary>
	/// Determines whether any entity exists that matches the specified predicate.
	/// </summary>
	/// <param name="predicate">An expression to filter the entities.</param>
	/// <param name="includeDeleted">
	/// If <see langword="true"/>, includes entities marked as deleted in the evaluation; otherwise, excludes them.
	/// </param>
	/// <param name="cancellationToken">The cancellation token for asynchronous operations.</param>
	/// <returns><see langword="true"/> if at least one matching entity exists; otherwise, <see langword="false"/>.</returns>
	ValueTask<bool> ExistsAsync(
		Expression<Func<TEntity, bool>> predicate,
		bool includeDeleted,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Determines whether any entity exists that matches the specified predicate.
	/// This overload defaults to not including deleted entities.
	/// </summary>
	/// <param name="predicate">An expression to filter the entities.</param>
	/// <param name="cancellationToken">The cancellation token for asynchronous operations.</param>
	/// <returns><see langword="true"/> if at least one matching entity exists; otherwise, <see langword="false"/>.</returns>
	public ValueTask<bool> ExistsAsync(
		Expression<Func<TEntity, bool>> predicate,
		CancellationToken cancellationToken = default) {
		return this.ExistsAsync(predicate, false, cancellationToken);
	}

	#endregion

	#region Count Methods

	/// <summary>
	/// Gets the total count of entities in the repository.
	/// </summary>
	/// <param name="includeDeleted">
	/// If <see langword="true"/>, counts entities marked as deleted as well; otherwise, excludes them.
	/// </param>
	/// <param name="cancellationToken">The cancellation token for asynchronous operations.</param>
	/// <returns>The total count of entities.</returns>
	ValueTask<int> CountAsync(
		bool includeDeleted,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets the total count of entities in the repository.
	/// This overload defaults to not including deleted entities.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token for asynchronous operations.</param>
	/// <returns>The total count of entities.</returns>
	public ValueTask<int> CountAsync(
		CancellationToken cancellationToken = default) {
		return this.CountAsync(false, cancellationToken);
	}

	/// <summary>
	/// Gets the count of entities that match the specified predicate.
	/// </summary>
	/// <param name="predicate">An expression to filter the entities.</param>
	/// <param name="includeDeleted">
	/// If <see langword="true"/>, includes entities marked as deleted in the count; otherwise, excludes them.
	/// </param>
	/// <param name="cancellationToken">The cancellation token for asynchronous operations.</param>
	/// <returns>The count of matching entities.</returns>
	ValueTask<int> CountAsync(
		Expression<Func<TEntity, bool>> predicate,
		bool includeDeleted,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets the count of entities that match the specified predicate.
	/// This overload defaults to not including deleted entities.
	/// </summary>
	/// <param name="predicate">An expression to filter the entities.</param>
	/// <param name="cancellationToken">The cancellation token for asynchronous operations.</param>
	/// <returns>The count of matching entities.</returns>
	public ValueTask<int> CountAsync(
		Expression<Func<TEntity, bool>> predicate,
		CancellationToken cancellationToken = default) {
		return this.CountAsync(predicate, false, cancellationToken);
	}

	#endregion

	#region Paging Methods

	/// <summary>
	/// Gets a page of entities using cursor-based (keyset) pagination.
	/// </summary>
	/// <param name="predicate">
	/// A filter for the paging operation. If <see langword="null"/>, retrieves all entities.
	/// </param>
	/// <param name="includeDeleted">
	/// If <see langword="true"/>, includes entities marked as deleted; otherwise, excludes them.
	/// </param>
	/// <param name="pageSize">The maximum number of entities to return.</param>
	/// <param name="cursor">A cursor from a previous page request, or <see langword="null"/> to start from the beginning.</param>
	/// <param name="cancellationToken">The cancellation token for asynchronous operations.</param>
	/// <returns>A <see cref="CursorResult{T}"/> containing the entities and pagination metadata.</returns>
	/// <remarks>
	/// <para>
	/// Cursor pagination provides stable results when data is being inserted or deleted, and performs
	/// consistently regardless of how deep into the result set the client navigates. This makes it
	/// ideal for large datasets, real-time data, and infinite scroll interfaces.
	/// </para>
	/// <para>
	/// For scenarios requiring arbitrary page jumps or total counts, consider 
	/// using <see cref="O:Cirreum.Persistence.IReadOnlyRepository`1.PageAsync"/> instead.
	/// </para>
	/// </remarks>
	ValueTask<CursorResult<TEntity>> PageCursorAsync(
		Expression<Func<TEntity, bool>>? predicate,
		bool includeDeleted,
		int pageSize,
		string? cursor,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets a page of entities using cursor-based (keyset) pagination.
	/// This overload defaults to not including deleted entities.
	/// </summary>
	/// <param name="predicate">
	/// A filter for the paging operation. If <see langword="null"/>, retrieves all entities.
	/// </param>
	/// <param name="pageSize">The maximum number of entities to return.</param>
	/// <param name="cursor">A cursor from a previous page request, or <see langword="null"/> to start from the beginning.</param>
	/// <param name="cancellationToken">The cancellation token for asynchronous operations.</param>
	/// <returns>A <see cref="CursorResult{T}"/> containing the entities and pagination metadata.</returns>
	/// <remarks>
	/// <para>
	/// Cursor pagination provides stable results when data is being inserted or deleted, and performs
	/// consistently regardless of how deep into the result set the client navigates. This makes it
	/// ideal for large datasets, real-time data, and infinite scroll interfaces.
	/// </para>
	/// <para>
	/// For scenarios requiring arbitrary page jumps or total counts, consider using <see cref="O:Cirreum.Persistence.IReadOnlyRepository`1.PageAsync"/> instead.
	/// </para>
	/// </remarks>
	public ValueTask<CursorResult<TEntity>> PageCursorAsync(
		Expression<Func<TEntity, bool>>? predicate,
		int pageSize,
		string? cursor,
		CancellationToken cancellationToken = default) {
		return this.PageCursorAsync(predicate, false, pageSize, cursor, cancellationToken);
	}

	/// <summary>
	/// Queries for a specific page of entities using offset-based pagination.
	/// </summary>
	/// <param name="predicate">
	/// A filter for the paging operation. If <see langword="null"/>, retrieves all entities.
	/// </param>
	/// <param name="includeDeleted">
	/// If <see langword="true"/>, includes entities marked as deleted; otherwise, excludes them.
	/// </param>
	/// <param name="pageNumber">The 1-based page number to retrieve.</param>
	/// <param name="pageSize">The maximum number of entities to return per page.</param>
	/// <param name="cancellationToken">The cancellation token for asynchronous operations.</param>
	/// <returns>A <see cref="PagedResult{T}"/> containing the entities and pagination metadata.</returns>
	/// <remarks>
	/// <para>
	/// Offset pagination allows clients to jump to arbitrary pages and includes total counts.
	/// This works well for smaller datasets and traditional paged interfaces with numbered pages.
	/// </para>
	/// <para>
	/// <strong>Trade-offs:</strong> Typically requires two queries (count + data), performance degrades
	/// on deep pages, and results can shift if data is inserted or deleted between requests.
	/// </para>
	/// <para>
	/// For large datasets, real-time data, or infinite scroll interfaces where consistency matters,
	/// consider using <see cref="O:Cirreum.Persistence.IReadOnlyRepository`1.PageCursorAsync"/> instead.
	/// </para>
	/// </remarks>
	ValueTask<PagedResult<TEntity>> PageAsync(
		Expression<Func<TEntity, bool>>? predicate,
		bool includeDeleted,
		int pageNumber,
		int pageSize,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Queries for a specific page of entities using offset-based pagination.
	/// This overload defaults to not including deleted entities.
	/// </summary>
	/// <param name="predicate">
	/// A filter for the paging operation. If <see langword="null"/>, retrieves all entities.
	/// </param>
	/// <param name="pageNumber">The 1-based page number to retrieve.</param>
	/// <param name="pageSize">The maximum number of entities to return per page.</param>
	/// <param name="cancellationToken">The cancellation token for asynchronous operations.</param>
	/// <returns>A <see cref="PagedResult{T}"/> containing the entities and pagination metadata.</returns>
	/// <remarks>
	/// <para>
	/// Offset pagination allows clients to jump to arbitrary pages and includes total counts.
	/// This works well for smaller datasets and traditional paged interfaces with numbered pages.
	/// </para>
	/// <para>
	/// <strong>Trade-offs:</strong> Typically requires two queries (count + data), performance degrades
	/// on deep pages, and results can shift if data is inserted or deleted between requests.
	/// </para>
	/// <para>
	/// For large datasets, real-time data, or infinite scroll interfaces where consistency matters,
	/// consider using <see cref="O:Cirreum.Persistence.IReadOnlyRepository`1.PageCursorAsync"/> instead.
	/// </para>
	/// </remarks>
	public ValueTask<PagedResult<TEntity>> PageAsync(
		Expression<Func<TEntity, bool>>? predicate,
		int pageNumber,
		int pageSize,
		CancellationToken cancellationToken = default) {
		return this.PageAsync(predicate, false, pageNumber, pageSize, cancellationToken);
	}

	/// <summary>
	/// Gets a slice of entities with an indicator for whether more items exist.
	/// </summary>
	/// <param name="predicate">
	/// A filter for the operation. If <see langword="null"/>, retrieves all entities.
	/// </param>
	/// <param name="includeDeleted">
	/// If <see langword="true"/>, includes entities marked as deleted; otherwise, excludes them.
	/// </param>
	/// <param name="count">The maximum number of entities to return.</param>
	/// <param name="cancellationToken">The cancellation token for asynchronous operations.</param>
	/// <returns>A <see cref="SliceResult{T}"/> containing the entities and whether more exist.</returns>
	/// <remarks>
	/// <para>
	/// Use this when you need a simple "load more" pattern without full pagination metadata.
	/// Typically implemented by fetching N+1 items and checking if more exist.
	/// </para>
	/// <para>
	/// This is ideal for scenarios where you don't need cursor stability or page numbers,
	/// such as loading initial data with a "Show More" button, or batch processing.
	/// </para>
	/// </remarks>
	ValueTask<SliceResult<TEntity>> SliceAsync(
		Expression<Func<TEntity, bool>>? predicate,
		bool includeDeleted,
		int count,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets a slice of entities with an indicator for whether more items exist.
	/// This overload defaults to not including deleted entities.
	/// </summary>
	/// <param name="predicate">
	/// A filter for the operation. If <see langword="null"/>, retrieves all entities.
	/// </param>
	/// <param name="count">The maximum number of entities to return.</param>
	/// <param name="cancellationToken">The cancellation token for asynchronous operations.</param>
	/// <returns>A <see cref="SliceResult{T}"/> containing the entities and whether more exist.</returns>
	/// <remarks>
	/// <para>
	/// Use this when you need a simple "load more" pattern without full pagination metadata.
	/// Typically implemented by fetching N+1 items and checking if more exist.
	/// </para>
	/// <para>
	/// This is ideal for scenarios where you don't need cursor stability or page numbers,
	/// such as loading initial data with a "Show More" button, or batch processing.
	/// </para>
	/// </remarks>
	public ValueTask<SliceResult<TEntity>> SliceAsync(
		Expression<Func<TEntity, bool>>? predicate,
		int count,
		CancellationToken cancellationToken = default) {
		return this.SliceAsync(predicate, false, count, cancellationToken);
	}

	#endregion

}