namespace Cirreum.Persistence;

using System.Text.Json.Serialization;

/// <summary>
/// A base Entity record, that can be persisted and de-persisted
/// from persistent storage. And implements <see cref="IEntity"/>,
/// <see cref="IEtagEntity"/>, <see cref="ITimeToLiveEntity"/>,
/// and <see cref="IAuditableEntity"/>.
/// </summary>
public abstract record Entity : BaseEntity, IEtagEntity, ITimeToLiveEntity, IAuditableEntity {

	/// <summary>
	/// Default constructor
	/// </summary>
	protected Entity() {
	}

	/// <summary>
	/// A constructor that allows the etag to be set so that items can be mapped to and from other objects
	/// </summary>
	/// <param name="etag"></param>
	protected Entity(string etag) {
		this.Etag = etag;
	}

	/// <inheritdoc/>
	[JsonPropertyName("_etag")]
	public string Etag { get; init; } = "";


	/// <inheritdoc/>
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