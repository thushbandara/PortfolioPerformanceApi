using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioPerformance.Data.Entities;

namespace PortfolioPerformance.Data.Configuration
{
    public class PortfolioConfiguration : BaseEntityConfiguration<Portfolio>
    {
        protected override void Configure(EntityTypeBuilder<Portfolio> builder)
        {
            builder.HasMany(p => p.Assets)
                .WithOne(a => a.Portfolio)
                .HasForeignKey(a => a.PortfolioId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
