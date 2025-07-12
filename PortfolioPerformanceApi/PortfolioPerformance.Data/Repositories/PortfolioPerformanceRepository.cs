using Microsoft.EntityFrameworkCore;
using PortfolioPerformance.Data.Contracts;
using System.Linq.Expressions;

namespace PortfolioPerformance.Data.Repositories
{
    /// <summary>
    /// Repository for managing entities in the Portfolio Performance data store.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public class PortfolioPerformanceRepository<TEntity>(PortfolioPerformanceContext context) : IPortfolioPerformanceRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// The set
        /// </summary>
        private readonly DbSet<TEntity> _set = context.Set<TEntity>();

        /// <summary>
        /// Adds a new entity to the data store.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        public async Task AddAsync(TEntity entity)
        {
            context.Add(entity);
            await SaveChangesAsync();
        }

        /// <summary>
        /// Finds entities matching the specified predicate.
        /// </summary>
        /// <param name="predicate">The expression used to filter entities.</param>
        /// <returns>
        /// A collection of matching entities.
        /// </returns>
        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        /// <summary>
        /// Retrieves all entities of type TEntity.
        /// </summary>
        /// <returns>
        /// A collection of all entities.
        /// </returns>
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await context.Set<TEntity>().ToListAsync();
        }

        /// <summary>
        /// Retrieves an entity by its unique Guid identifier.
        /// </summary>
        /// <param name="id">The Guid identifier of the entity.</param>
        /// <returns>
        /// The entity if found; otherwise, null.
        /// </returns>
        public async Task<TEntity?> GetByIdAsync(Guid id)
        {
            return await context.Set<TEntity>()
                        .AsNoTracking()
                        .FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="includes">The includes.</param>
        /// <returns>
        /// The entity if found; otherwise, null.
        /// </returns>
        public async Task<TEntity?> GetByIdAsync(Guid id, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _set.AsNoTracking();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);
        }

        /// <summary>
        /// Removes an entity from the data store.
        /// </summary>
        /// <param name="entity">The entity to remove.</param>
        public async Task Remove(TEntity entity)
        {
            context.Remove(entity);
            await SaveChangesAsync();
        }


        /// <summary>
        /// Updates an existing entity in the data store.
        /// </summary>
        /// <param name="entity">The entity with updated values.</param>
        public async Task Update(TEntity entity)
        {
            context.Set<TEntity>().Update(entity);
            context.Entry(entity).State = EntityState.Modified;
            await SaveChangesAsync();
        }

        /// <summary>
        /// Persists all pending changes to the data store.
        /// </summary>
        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

    }
}
