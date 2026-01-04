namespace Cirreum.Persistence;

/// <summary>
/// The base interface that defines an Entity.
/// </summary>
public interface IEntity {

	/// <summary>
	/// Gets or sets the entity's globally unique identifier.
	/// </summary>
	string Id { get; set; }

	/// <summary>
	/// Gets the entity's Type name.
	/// </summary>
	string EntityType { get; init; }

	/// <summary>
	/// Gets the entity's PartitionKey.
	/// </summary>
	string PartitionKey { get; }

}