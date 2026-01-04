namespace Cirreum.Persistence;
/// <summary>
/// Extends <see cref="IContinuationPage{T}"/> and represents a page of data
/// with support for offset-based paging.
/// </summary>
/// <typeparam name="T"></typeparam>
/// <remarks>
/// <para>
/// Offset paging will incur more RU's then continuation token paging.
/// </para>
/// </remarks>
public interface IOffSetPage<out T> : IContinuationPage<T> where T : IEntity {

	/// <summary>
	/// The total number of pages that matched the query.
	/// </summary>
	int? TotalPages { get; }

	/// <summary>
	/// The current page number this result represents.
	/// </summary>
	int? PageNumber { get; }

	/// <summary>
	/// Determines if there is a previous page based on current page number
	/// </summary>
	bool HasPreviousPage => this.PageNumber > 1;

	/// <summary>
	/// Determines if there is a next page based on current page number and total pages
	/// </summary>
	public bool? HasNextPage => this.TotalPages is not null ? this.PageNumber < this.TotalPages : null;

	/// <summary>
	/// Gets the previous page number, or 1 if on first page
	/// </summary>
	int PreviousPageNumber => this.HasPreviousPage && this.PageNumber.HasValue ? this.PageNumber.Value - 1 : 1;

	/// <summary>
	/// Gets the next page number, or TotalPages if on last page
	/// </summary>
	int? NextPageNumber =>
		this.HasNextPage == true && this.PageNumber.HasValue
		? this.PageNumber.Value + 1
		: this.TotalPages;

	/// <summary>
	/// Determines if this is the first page
	/// </summary>
	bool? IsFirstPage => this.PageNumber is not null ? this.PageNumber == 1 : null;

}