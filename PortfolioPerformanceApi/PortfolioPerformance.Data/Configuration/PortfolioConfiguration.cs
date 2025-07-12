using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioPerformance.Data.Entities;

namespace PortfolioPerformance.Data.Configuration
{
    /// <summary>
    /// Configuration for the <see cref="Portfolio"/> entity.
    /// </summary>
    public class PortfolioConfiguration : BaseEntityConfiguration<Portfolio>
    {
        /// <summary>
        /// Configures the entity of type <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        protected override void Configure(EntityTypeBuilder<Portfolio> builder)
        {
            builder.HasMany(p => p.Assets)
                .WithOne(a => a.Portfolio)
                .HasForeignKey(a => a.PortfolioId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
