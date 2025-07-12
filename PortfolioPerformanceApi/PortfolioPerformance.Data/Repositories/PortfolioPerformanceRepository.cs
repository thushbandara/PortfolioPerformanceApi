using Microsoft.EntityFrameworkCore;
using PortfolioPerformance.Data.Contracts;
using System.Linq.Expressions;

namespace PortfolioPerformance.Data.Repositories
{
    public class PortfolioPerformanceRepository<TEntity>(PortfolioPerformanceContext context) : IPortfolioPerformanceRepository<TEntity> where TEntity : class
    {
         private readonly DbSet<TEntity> _set = context.Set<TEntity>();

        public async Task AddAsync(TEntity entity)
        {
            context.Add(entity);
            await SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(Guid id)
        {
            return await context.Set<TEntity>()
                        .AsNoTracking()
                        .FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);
        }

        public async Task<TEntity?> GetByIdAsync(Guid id, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _set.AsNoTracking();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);
        }

        public async Task Remove(TEntity entity)
        {
            context.Remove(entity);
            await SaveChangesAsync();
        }


        public async Task Update(TEntity entity)
        {
            context.Set<TEntity>().Update(entity);
            context.Entry(entity).State = EntityState.Modified;
            await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

    }
}
