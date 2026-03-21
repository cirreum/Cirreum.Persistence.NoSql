namespace Cirreum.Persistence;

/// <summary>
/// Specifies the supported indexing modes.
/// </summary>
public enum IndexingMode {

	/// <summary>
	/// Index is updated synchronously with create, update, or delete operations.
	/// </summary>
	Consistent = 0,

	/// <summary>
	/// Index is updated asynchronously.
	/// </summary>
	Lazy = 1,

	/// <summary>
	/// No index is provided.
	/// </summary>
	None = 2
}
