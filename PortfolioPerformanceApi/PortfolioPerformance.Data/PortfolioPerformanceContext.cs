using Microsoft.EntityFrameworkCore;
using PortfolioPerformance.Data.Configuration;
using PortfolioPerformance.Data.Entities;

namespace PortfolioPerformance.Data
{
    public class PortfolioPerformanceContext(DbContextOptions<PortfolioPerformanceContext> options) : DbContext(options)
    {
        public DbSet<Portfolio> Portfolios => Set<Portfolio>();
        public DbSet<Asset> Assets => Set<Asset>();
        public DbSet<Transaction> Transactions => Set<Transaction>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PortfolioConfiguration());
            modelBuilder.ApplyConfiguration(new AssetConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
        }
    }
}