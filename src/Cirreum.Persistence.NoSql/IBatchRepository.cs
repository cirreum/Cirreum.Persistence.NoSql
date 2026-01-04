namespace Cirreum.Persistence;

using System.Collections.Generic;
using System.Threading.Tasks;

/// <summary>
/// A repository interface for Batch create, update and delete operations.
/// </summary>
/// <typeparam name="TEntity">The <see cref="IEntity"/> implementation class type.</typeparam>
public interface IBatchRepository<in TEntity> where TEntity : IEntity {

	/// <summary>
	/// Updates an <see cref="IEnumerable{TEntity}"/> as a batch.
	/// </summary>
	/// <param name="items">The items to update.</param>
	/// <param name="cancellationToken">A token to cancel the async operation.</param>
	/// <returns>An <see cref="ValueTask"/> that represents the async batch operation.</returns>
	/// <remarks>
	/// <para>
	/// All items must share the same partition.
	/// </para>
	/// </remarks>
	ValueTask UpdateAsBatchAsync(
		IEnumerable<TEntity> items,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Creates an <see cref="IEnumerable{TEntity}"/> as a batch.
	/// </summary>
	/// <param name="items">The items to create.</param>
	/// <param name="cancellationToken">A token to cancel the async operation.</param>
	/// <returns>An <see cref="ValueTask"/> that represents the async batch operation.</returns>
	/// <remarks>
	/// <para>
	/// All items must share the same partition.
	/// </para>
	/// </remarks>
	ValueTask CreateAsBatchAsync(
		IEnumerable<TEntity> items,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Deletes an <see cref="IEnumerable{TEntity}"/> as a batch.
	/// </summary>
	/// <param name="items">The items to create.</param>
	/// <param name="cancellationToken">A token to cancel the async operation.</param>
	/// <returns>An <see cref="ValueTask"/> that represents the async batch operation.</returns>
	/// <remarks>
	/// <para>
	/// All items must share the same partition.
	/// </para>
	/// </remarks>
	ValueTask DeleteAsBatchAsync(
		IEnumerable<TEntity> items,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Deletes an <see cref="IEnumerable{TEntity}"/> as a batch.
	/// </summary>
	/// <param name="items">The items to create.</param>
	/// <param name="cancellationToken">A token to cancel the async operation.</param>
	/// <param name="softDelete">When <see langword="true"/> and the <paramref name="items"/> implement <see cref="IDeletableEntity"/>, performs a soft-delete.</param>
	/// <returns>An <see cref="ValueTask"/> that represents the async batch operation.</returns>
	/// <remarks>
	/// <para>
	/// All items must share the same partition.
	/// </para>
	/// </remarks>
	ValueTask DeleteAsBatchAsync(
		IEnumerable<TEntity> items,
		CancellationToken cancellationToken,
		bool softDelete = true);

	/// <summary>
	/// Attempts to restore a batch of soft-deleted entities to an active state.
	/// </summary>
	/// <param name="items">The items to restore.</param>
	/// <param name="cancellationToken">A token to cancel the async operation.</param>
	/// <returns>A collection of tuples containing the entity ID and whether it was restored (true) or skipped because it was not deleted (false).</returns>
	/// <remarks>
	/// <para>
	/// All items must share the same partition.
	/// </para>
	/// </remarks>
	ValueTask<IEnumerable<(string Id, bool Restored)>> RestoreAsBatchAsync(
		IEnumerable<TEntity> items,
		CancellationToken cancellationToken = default);

}