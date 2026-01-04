namespace Cirreum.Persistence;

/// <summary>
/// Defines an entity that supports restoration from a soft-deleted state.
/// Extends <see cref="IDeletableEntity"/> to add restoration tracking capabilities.
/// </summary>
/// <remarks>
/// Implementing this interface indicates that an entity can be restored after being soft-deleted.
/// The <see cref="RestoreCount"/> property tracks how many times the entity has been restored,
/// which can be useful for auditing and implementing business rules around restoration limits.
/// </remarks>
public interface IRestorableEntity : IDeletableEntity {
	/// <summary>
	/// Gets or sets the number of times this entity has been restored from a soft-deleted state.
	/// </summary>
	/// <value>
	/// A non-negative integer representing the number of successful restore operations performed on this entity.
	/// A value of 0 indicates the entity has never been restored.
	/// </value>
	int RestoreCount { get; set; }
}