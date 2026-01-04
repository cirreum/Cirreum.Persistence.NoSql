namespace Cirreum.Persistence;

/// <summary>
/// The base interface that defines an Entity that supports auditing by extending
/// the <see cref="IEntity"/> interface with the addition of the
/// <see cref="CreatedOn"/>, <see cref="CreatedBy"/>, 
/// <see cref="ModifiedOnRaw"/>, <see cref="ModifiedBy "/>
/// and <see cref="ModifiedBy "/> properties.
/// </summary>
public interface IAuditableEntity : IEntity {

	/// <summary>
	/// Time stamp of when the entity was created.
	/// </summary>
	DateTimeOffset? CreatedOn { get; set; }

	/// <summary>
	/// Name of the user that created this entity.
	/// </summary>
	string? CreatedBy { get; set; }

	/// <summary>
	/// The IANA time zone identifier of the server where the entity was created (e.g., "America/New_York").
	/// </summary>
	/// <remarks>
	/// <para>
	/// This property stores the IANA format time zone ID of the server environment when the entity 
	/// was last modified. By default, this is the server's time zone, but it could be explicitly set to 
	/// the user's time zone if that information is included in the modification command.
	/// When null, the time zone is unknown or wasn't recorded.
	/// </para>
	/// <para>
	/// Example: "America/Phoenix", "Europe/London", "Asia/Tokyo", etc.
	/// </para>
	/// </remarks>
	string? CreatedInTimeZone { get; set; }

	/// <summary>
	/// Time stamp of when the entity was last modified.
	/// </summary>
	/// <remarks>
	/// <para>
	/// Typically this would not be persisted to the provider, but merely
	/// expose the <see cref="ModifiedOnRaw"/> value as a <see cref="DateTimeOffset"/>.
	/// </para>
	/// <para>
	/// Implmentation will usual exclude using <c>[JsonIgnore]</c>.
	/// </para>
	/// </remarks>
	DateTimeOffset ModifiedOn { get; }

	/// <summary>
	/// The IANA time zone identifier of the server where the entity was last modified (e.g., "America/Los_Angeles").
	/// </summary>
	/// <remarks>
	/// <para>
	/// This property stores the IANA format time zone ID of the user's location when the entity 
	/// was last modified. It provides additional context for the <see cref="ModifiedOn"/> timestamp.
	/// When null, the time zone is unknown or wasn't recorded.
	/// </para>
	/// <para>
	/// Example: "America/Phoenix", "Europe/London", "Asia/Tokyo", etc.
	/// </para>
	/// </remarks>
	string? ModifiedInTimeZone { get; set; }

	/// <summary>
	/// Name of the user who last modified this entity.
	/// </summary>
	string ModifiedBy { get; set; }

	/// <summary>
	/// The raw timestamp from the persistence provider.
	/// </summary>
	long ModifiedOnRaw { get; }

}