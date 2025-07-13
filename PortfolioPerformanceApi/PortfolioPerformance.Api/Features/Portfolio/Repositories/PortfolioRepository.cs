using Microsoft.EntityFrameworkCore;
using PortfolioPerformance.Data;

namespace PortfolioPerformance.Api.Features.Portfolio.Repositories
{
    /// <summary>
    /// Repository interface for managing portfolio data in the Portfolio Performance system.
    /// </summary>
    public interface IPortfolioRepository
    {
        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<Data.Entities.Portfolio?> GetByIdAsync(Guid id);
    }

    /// <summary>
    /// Repository for managing portfolio data in the Portfolio Performance system.
    /// </summary>
    public class PortfolioRepository(PortfolioPerformanceContext context) : IPortfolioRepository
    {

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<Data.Entities.Portfolio?> GetByIdAsync(Guid id)
        {
            return await context.Portfolios
                              .Include(p => p.Assets)
                              .ThenInclude(a => a.Transactions)
                              .AsNoTracking()
                              .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
