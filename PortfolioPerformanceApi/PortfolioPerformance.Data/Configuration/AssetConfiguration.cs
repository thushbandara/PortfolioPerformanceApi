using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioPerformance.Data.Entities;

namespace PortfolioPerformance.Data.Configuration
{
    /// <summary>
    /// Configuration for the Asset entity in the portfolio performance system.
    /// </summary>
    public class AssetConfiguration : BaseEntityConfiguration<Asset>
    {
        /// <summary>
        /// Configures the specified builder.
        /// </summary>
        /// <param name="builder">The builder.</param>
        protected override void Configure(EntityTypeBuilder<Asset> builder)
        {
            builder.HasMany(a => a.Transactions)
                .WithOne(t => t.Asset)
                .HasForeignKey(t => t.AssetId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
