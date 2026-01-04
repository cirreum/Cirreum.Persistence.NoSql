namespace Cirreum.Persistence;

/// <summary>
/// A consolidated repository interface, joining <see cref="IReadOnlyRepository{TEntity}"/>,
/// <see cref="IWriteOnlyRepository{TEntity}"/> and <see cref="IBatchRepository{TEntity}"/>.
/// </summary>
/// <typeparam name="TEntity">The <see cref="IEntity"/> implementation class type.</typeparam>
public interface IRepository<TEntity> :
	IReadOnlyRepository<TEntity>,
	IWriteOnlyRepository<TEntity>,
	IBatchRepository<TEntity>
	where TEntity : IEntity;