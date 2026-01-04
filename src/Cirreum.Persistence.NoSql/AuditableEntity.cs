namespace Cirreum.Persistence;

using System.Text.Json.Serialization;

/// <summary>
/// An Entity record, that can be persisted and de-persisted
/// from persistent storage. Extends <see cref="BaseEntity"/>
/// by implementing <see cref="IAuditableEntity"/>.
/// </summary>
public abstract record AuditableEntity : BaseEntity, IAuditableEntity {

	/// <inheritdoc/>
	public DateTimeOffset? CreatedOn { get; set; }

	/// <inheritdoc/>
	public string? CreatedBy { get; set; }

	/// <inheritdoc/>
	public string? CreatedInTimeZone { get; set; }

	/// <inheritdoc/>
	[JsonIgnore]
	public DateTimeOffset ModifiedOn => DateTimeOffset.FromUnixTimeSeconds(this.ModifiedOnRaw);

	/// <inheritdoc/>
	public string ModifiedBy { get; set; } = "";

	/// <inheritdoc/>
	public string? ModifiedInTimeZone { get; set; }

	/// <inheritdoc/>
	[JsonPropertyName("_ts")]
	public long ModifiedOnRaw { get; init; }

}