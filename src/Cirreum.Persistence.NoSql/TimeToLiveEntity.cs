namespace Cirreum.Persistence;

using System.Text.Json.Serialization;

/// <summary>
/// An Entity record, that can be persisted and de-persisted
/// from persistent storage. Extends <see cref="BaseEntity"/>
/// by implementing <see cref="ITimeToLiveEntity"/>.
/// </summary>
public abstract record TimeToLiveEntity : BaseEntity, ITimeToLiveEntity {

	[JsonIgnore]
	public TimeSpan? TimeToLive {
		get => this._timeToLive.HasValue ? TimeSpan.FromSeconds(this._timeToLive.Value) : null;
		set {
			if (value is not null) {
				this._timeToLive = (int)value.Value.TotalSeconds;
			}
		}
	}

	[JsonPropertyName("ttl")]
	private int? _timeToLive;

}