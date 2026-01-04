namespace Cirreum.Persistence;

/// <summary>
/// Extends <see cref="IQueryResult{TEntity}"/> and represents a page of data
/// with support for paging using a continuation token.
/// </summary>
/// <typeparam name="T">The type of entity contained in the page.</typeparam>
public interface IContinuationPage<out T> : IQueryResult<T> where T : IEntity {

	/// <summary>
	/// Optionally, gets the total number of items that matched the query.
	/// </summary>
	int? Total { get; }

	/// <summary>
	/// Gets the number of entities in the current page.
	/// </summary>
	int Size { get; }

	/// <summary>
	/// Gets the continuation token used to retrieve the next set of results.
	/// </summary>
	/// <remarks>
	/// This token is provided by the query engine, and will be <see langword="null"/> when there are no further results.
	/// </remarks>
	string? Continuation { get; }

}