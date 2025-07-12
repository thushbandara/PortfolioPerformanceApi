using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioPerformance.Data.Entities;

namespace PortfolioPerformance.Data.Configuration
{
    public class TransactionConfiguration : BaseEntityConfiguration<Transaction>
    {
        protected override void Configure(EntityTypeBuilder<Transaction> builder)
        {

        }
    }
}
