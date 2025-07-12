using System.Linq.Expressions;

namespace PortfolioPerformance.Data.Contracts
{
    public interface IPortfolioPerformanceRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Retrieves an entity by its unique Guid identifier.
        /// </summary>
        /// <param name="id">The Guid identifier of the entity.</param>
        /// <returns>The entity if found; otherwise, null.</returns>
        Task<TEntity?> GetByIdAsync(Guid id);

        /// <summary>
        /// Retrieves all entities of type TEntity.
        /// </summary>
        /// <returns>A collection of all entities.</returns>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Finds entities matching the specified predicate.
        /// </summary>
        /// <param name="predicate">The expression used to filter entities.</param>
        /// <returns>A collection of matching entities.</returns>
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Adds a new entity to the data store.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        Task AddAsync(TEntity entity);

        /// <summary>
        /// Updates an existing entity in the data store.
        /// </summary>
        /// <param name="entity">The entity with updated values.</param>
        Task Update(TEntity entity);

        /// <summary>
        /// Removes an entity from the data store.
        /// </summary>
        /// <param name="entity">The entity to remove.</param>
        Task Remove(TEntity entity);

        /// <summary>
        /// Persists all pending changes to the data store.
        /// </summary>
        Task SaveChangesAsync();
    }
}
