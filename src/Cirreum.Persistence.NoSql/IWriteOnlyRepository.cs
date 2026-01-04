namespace Cirreum.Persistence;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// A repository interface for Write Only operations.
/// </summary>
/// <typeparam name="TEntity">The <see cref="IEntity"/> implementation class type.</typeparam>
public interface IWriteOnlyRepository<TEntity> where TEntity : IEntity {

	/// <summary>
	/// Creates an entity representing the given <paramref name="value"/>.
	/// </summary>
	/// <param name="value">The entity value to create.</param>
	/// <param name="cancellationToken">The cancellation token to use when making asynchronous operations.</param>
	/// <returns>A <see cref="ValueTask{TEntity}"/> representing the <see cref="IEntity"/> implementation class instance as a <typeparamref name="TEntity"/>.</returns>
	ValueTask<TEntity> CreateAsync(
		TEntity value,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Creates one or more entities representing the given <paramref name="values"/>.
	/// </summary>
	/// <param name="values">The entity values to create.</param>
	/// <param name="cancellationToken">The cancellation token to use when making asynchronous operations.</param>
	/// <returns>A collection of created entity instances.</returns>
	ValueTask<IEnumerable<TEntity>> CreateAsync(
		IEnumerable<TEntity> values,
		CancellationToken cancellationToken = default);



	/// <summary>
	/// Updates the entity that corresponds to the given <paramref name="value"/>.
	/// </summary>
	/// <param name="value">The entity value to update.</param>
	/// <param name="cancellationToken">The cancellation token to use when making asynchronous operations.</param>
	/// <returns>A <see cref="ValueTask{TEntity}"/> representing the <see cref="IEntity"/> implementation class instance as a <typeparamref name="TEntity"/>.</returns>
	ValueTask<TEntity> UpdateAsync(
		TEntity value,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Updates the entity that corresponds to the given <paramref name="value"/>.
	/// </summary>
	/// <param name="value">The entity value to update.</param>
	/// <param name="cancellationToken">The cancellation token to use when making asynchronous operations.</param>
	/// <param name="ignoreEtag">When TEntity implements IItemWithEtag the etag will be verified on all updates. Setting this flag to true indicates that the etag should be ignored.</param>
	/// <returns>A <see cref="ValueTask{TEntity}"/> representing the <see cref="IEntity"/> implementation class instance as a <typeparamref name="TEntity"/>.</returns>
	ValueTask<TEntity> UpdateAsync(
		TEntity value,
		bool ignoreEtag = false,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Updates one or more entities representing the given <paramref name="values"/>.
	/// </summary>
	/// <param name="values">The entity values to update.</param>
	/// <param name="cancellationToken">The cancellation token to use when making asynchronous operations.</param>
	/// <param name="ignoreEtag">When TEntity implements IItemWithEtag the etag will be verified on all updates. Setting this flag to true indicates that the etag should be ignored.</param>
	/// <returns>A collection of updated entity instances.</returns>
	ValueTask<IEnumerable<TEntity>> UpdateAsync(
		IEnumerable<TEntity> values,
		bool ignoreEtag = false,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Applies partial updates to an entity with the specified identifier.
	/// </summary>
	/// <param name="id">The string identifier of the entity to update.</param>
	/// <param name="operations">An action that defines the specific update operations to perform.</param>
	/// <param name="concurrencyToken">Optional concurrency token to ensure the entity hasn't been modified since it was last retrieved. This requires TEntity to implement the IItemWithEtag interface.</param>
	/// <param name="cancellationToken">The cancellation token to use when making asynchronous operations.</param>
	/// <returns>A <see cref="ValueTask"/> representing the asynchronous operation.</returns>
	ValueTask UpdatePartialAsync(
		string id,
		Action<IPatchOperationBuilder<TEntity>> operations,
		string? concurrencyToken = default,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Attempts to restore a soft-deleted entity to an active state.
	/// </summary>
	/// <param name="id">The string identifier of the entity to restore.</param>
	/// <param name="cancellationToken">The cancellation token to use when making asynchronous operations.</param>
	/// <returns>
	/// A <see cref="ValueTask{TResult}"/> containing a tuple with:
	/// <list type="bullet">
	/// <item><description>A boolean indicating whether the restore operation was performed (true) or skipped because the entity was not deleted (false)</description></item>
	/// <item><description>The entity, either restored or in its current state</description></item>
	/// </list>
	/// </returns>
	/// <exception cref="InvalidOperationException">Thrown when the entity type does not implement <see cref="IRestorableEntity"/>.</exception>
	/// <remarks>
	/// This method will:
	/// <list type="bullet">
	/// <item><description>Skip restoration if the entity is not currently soft-deleted</description></item>
	/// <item><description>Clear the deletion metadata (DeletedBy, DeletedOn) if restored</description></item>
	/// <item><description>Increment the <see cref="IRestorableEntity.RestoreCount"/> if restored</description></item>
	/// </list>
	/// </remarks>
	ValueTask<(bool Restored, TEntity Entity)> RestoreAsync(
		string id,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// When <typeparamref name="TEntity"/> implements <see cref="IDeletableEntity"/>, Soft-Deletes the entity that corresponds to the given <paramref name="value"/>.
	/// </summary>
	/// <param name="value">The entity to delete.</param>
	/// <param name="cancellationToken">The cancellation token to use when making asynchronous operations.</param>
	/// <returns>A <see cref="ValueTask"/> representing the asynchronous delete operation.</returns>
	ValueTask DeleteAsync(
		TEntity value,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// When <typeparamref name="TEntity"/> implements <see cref="IDeletableEntity"/>, Soft-Deletes the entity that corresponds to the given <paramref name="id"/>.
	/// </summary>
	/// <param name="id">The string identifier.</param>
	/// <param name="cancellationToken">The cancellation token to use when making asynchronous operations.</param>
	/// <returns>A <see cref="ValueTask"/> representing the asynchronous delete operation.</returns>
	ValueTask DeleteAsync(
		string id,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// When <paramref name="softDelete"/> is <see langword="true"/> and <typeparamref name="TEntity"/> implements <see cref="IDeletableEntity"/>, Soft-Deletes the entity that corresponds to the given <paramref name="value"/>.
	/// </summary>
	/// <param name="value">The entity to delete.</param>
	/// <param name="cancellationToken">The cancellation token to use when making asynchronous operations.</param>
	/// <param name="softDelete">When <see langword="true"/> and <typeparamref name="TEntity"/> implements <see cref="IDeletableEntity"/>, performs a soft delete instead of a hard delete.</param>
	/// <returns>A <see cref="ValueTask{TEntity}"/> representing the deleted entity.</returns>
	ValueTask<TEntity> DeleteAsync(
		TEntity value,
		CancellationToken cancellationToken,
		bool softDelete = true);

	/// <summary>
	/// When <paramref name="softDelete"/> is <see langword="true"/> and <typeparamref name="TEntity"/> implements <see cref="IDeletableEntity"/>, Soft-Deletes the entity that corresponds to the given <paramref name="id"/>.
	/// </summary>
	/// <param name="id">The string identifier.</param>
	/// <param name="cancellationToken">The cancellation token to use when making asynchronous operations.</param>
	/// <param name="softDelete">When <see langword="true"/> and <typeparamref name="TEntity"/> implements <see cref="IDeletableEntity"/>, performs a soft delete instead of a hard delete.</param>
	/// <returns>A <see cref="ValueTask{TEntity}"/> representing the deleted entity.</returns>
	ValueTask<TEntity> DeleteAsync(
		string id,
		CancellationToken cancellationToken,
		bool softDelete = true);

}