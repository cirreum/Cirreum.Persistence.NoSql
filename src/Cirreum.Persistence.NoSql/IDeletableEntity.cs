namespace Cirreum.Persistence;

using System;

/// <summary>
/// The base interface that defines an Entity that supports soft deletes
/// by extending the <see cref="IEntity"/> interface with the addition
/// of the <see cref="DeletedBy"/>, <see cref="DeletedOn"/>
/// and <see cref="IsDeleted"/> properties.
/// </summary>
public interface IDeletableEntity : IEntity {

	/// <summary>
	/// Name of the user that deleted this entity.
	/// </summary>
	string DeletedBy { get; set; }

	/// <summary>
	/// Timestamp when it was deleted.
	/// </summary>
	DateTimeOffset? DeletedOn { get; set; }

	/// <summary>
	/// The IANA time zone identifier of the server where the entity was deleted (e.g., "America/Los_Angeles").
	/// </summary>
	/// <remarks>
	/// <para>
	/// This property stores the IANA format time zone ID of the location when the entity 
	/// was last deleted. It provides additional context for the <see cref="DeletedOn"/> timestamp.
	/// When null, the time zone is unknown or wasn't recorded.
	/// </para>
	/// <para>
	/// Example: "America/Phoenix", "Europe/London", "Asia/Tokyo", etc.
	/// </para>
	/// </remarks>
	string? DeletedInTimeZone { get; set; }

	/// <summary>
	/// <see langword="true"/> if this entity has been deleted.
	/// </summary>
	bool IsDeleted { get; set; }

}