namespace Cirreum.Persistence;

/// <summary>
/// The base interface that defines an entity with a TTL.
/// </summary>
public interface ITimeToLiveEntity : IEntity {

	/// <summary>
	/// The amount of time an entity should live/exist in persisted storage.
	/// </summary>
	public TimeSpan? TimeToLive { get; set; }

}