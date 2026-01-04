namespace Cirreum.Persistence;

/// <summary>
/// The base interface that defines an entity with an eTag.
/// </summary>
public interface IEtagEntity : IEntity {

	/// <summary>
	/// Etag for the entity which was set by the persistence provider
	/// when initially ceated, or the last time it was modified.
	/// </summary>
	string Etag { get; }

}