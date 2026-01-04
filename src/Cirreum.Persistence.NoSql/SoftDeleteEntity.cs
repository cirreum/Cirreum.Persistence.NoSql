namespace Cirreum.Persistence;

/// <summary>
/// A concrete implementation of an <see cref="Entity"/>, that also supports SoftDeletes via
/// the <see cref="IRestorableEntity"/> interface which extends the <see cref="IDeletableEntity"/>
/// interface.
/// </summary>
public abstract record SoftDeleteEntity : Entity, IRestorableEntity {

	/// <inheritdoc/>
	public string DeletedBy { get; set; } = "";

	/// <inheritdoc/>
	public DateTimeOffset? DeletedOn { get; set; }

	/// <inheritdoc/>
	public string? DeletedInTimeZone { get; set; }

	/// <inheritdoc/>
	public bool IsDeleted { get; set; }

	/// <inheritdoc/>
	public int RestoreCount { get; set; }

}