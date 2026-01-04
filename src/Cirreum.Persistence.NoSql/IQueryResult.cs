namespace Cirreum.Persistence;

using System.Collections.Generic;

/// <summary>
/// Represent a collection of entities, resulting from a query
/// or read operation.
/// </summary>
/// <typeparam name="TEntity">The Type of entities contained in the result.</typeparam>
public interface IQueryResult<out TEntity> where TEntity : IEntity {

	/// <summary>
	/// The entities that are in this result.
	/// </summary>
	IReadOnlyList<TEntity> Entities { get; }

	/// <summary>
	/// The estimated query cost.
	/// </summary>
	double Charge { get; }

}