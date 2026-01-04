namespace Cirreum.Persistence;

using System.Diagnostics;
using System.Text.Json.Serialization;

/// <summary>
/// A base Entity record, that can be persisted and de-persisted
/// from persistent storage, implementing <see cref="IEntity"/>.
/// </summary>
[DebuggerDisplay("Id = {" + nameof(Id) + "}")]
public abstract record BaseEntity : IEntity {

	/// <inheritdoc />
	/// <remarks>
	/// Initialized with <see cref="Guid.NewGuid"/>.ToString().
	/// </remarks>
	public string Id { get; set; } = Guid.NewGuid().ToString();

	public string EntityType { get; init; }

	[JsonIgnore]
	string IEntity.PartitionKey => this.GetPartitionKeyValue();

	/// <summary>
	/// Gets the partition key value for the given <see cref="Entity"/> type.
	/// When overridden, be sure that the <see cref="PartitionKeyPathAttribute.Path"/> value corresponds
	/// to the <see cref="JsonPropertyNameAttribute.Name"/> value, i.e.; "/partition" and "partition"
	/// respectively. If these two values do not correspond an error will occur.
	/// </summary>
	/// <returns>The <see cref="Id"/> unless overridden by the subclass.</returns>
	protected virtual string GetPartitionKeyValue() => this.Id;

	/// <summary>
	/// Default constructor.
	/// </summary>
	/// <remarks>
	/// Auto assigns the <see cref="Type"/>.<c>Name</c> to the <see cref="EntityType"/> property
	/// </remarks>
	public BaseEntity() => this.EntityType = this.GetType().Name;

}