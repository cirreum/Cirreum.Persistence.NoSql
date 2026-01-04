namespace Cirreum.Persistence;

using System.Text.Json.Serialization;

/// <summary>
/// An Entity record, that can be persisted and de-persisted
/// from persistent storage. Extends <see cref="BaseEntity"/>
/// by implementing <see cref="IEtagEntity"/>.
/// </summary>
public abstract record EtagEntity : BaseEntity, IEtagEntity {

	/// <summary>
	/// Default constructor
	/// </summary>
	protected EtagEntity() {
	}

	/// <summary>
	/// A constructor that allows the etag to be set so that items can be mapped to and from other objects
	/// </summary>
	/// <param name="etag"></param>
	protected EtagEntity(string etag) {
		this.Etag = etag;
	}

	/// <inheritdoc/>
	[JsonPropertyName("_etag")]
	public string Etag { get; init; } = string.Empty;

}