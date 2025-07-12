using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioPerformance.Data.Entities;

namespace PortfolioPerformance.Data.Configuration
{
    /// <summary>
    /// Configuration for the <see cref="Transaction"/> entity.
    /// </summary>
    public class TransactionConfiguration : BaseEntityConfiguration<Transaction>
    {
        /// <summary>
        /// Configures the entity of type <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        protected override void Configure(EntityTypeBuilder<Transaction> builder)
        {

        }
    }
}
