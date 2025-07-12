using Microsoft.EntityFrameworkCore;
using PortfolioPerformance.Data.Configuration;
using PortfolioPerformance.Data.Entities;

namespace PortfolioPerformance.Data
{
    /// <summary>
    /// Represents the context for the Portfolio Performance data store, managing entities such as portfolios, assets, and transactions.
    /// </summary>
    public class PortfolioPerformanceContext(DbContextOptions<PortfolioPerformanceContext> options) : DbContext(options)
    {
        /// <summary>
        /// Gets the portfolios.
        /// </summary>
        /// <value>
        /// The portfolios.
        /// </value>
        public DbSet<Portfolio> Portfolios => Set<Portfolio>();
        /// <summary>
        /// Gets the assets.
        /// </summary>
        /// <value>
        /// The assets.
        /// </value>
        public DbSet<Asset> Assets => Set<Asset>();
        /// <summary>
        /// Gets the transactions.
        /// </summary>
        /// <value>
        /// The transactions.
        /// </value>
        public DbSet<Transaction> Transactions => Set<Transaction>();

        /// <summary>
        /// Override this method to further configure the model that was discovered by convention from the entity types
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PortfolioConfiguration());
            modelBuilder.ApplyConfiguration(new AssetConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
        }
    }
}