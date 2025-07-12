using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioPerformance.Data.Entities;

namespace PortfolioPerformance.Data.Configuration
{
    public class AssetConfiguration : BaseEntityConfiguration<Asset>
    {
        protected override void Configure(EntityTypeBuilder<Asset> builder)
        {
            builder.HasMany(a => a.Transactions)
                .WithOne(t => t.Asset)
                .HasForeignKey(t => t.AssetId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
