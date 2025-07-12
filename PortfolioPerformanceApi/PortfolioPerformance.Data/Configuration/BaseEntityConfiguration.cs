using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioPerformance.Data.Entities;

namespace PortfolioPerformance.Data.Configuration
{
    public abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        void IEntityTypeConfiguration<T>.Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.CreatedBy).ValueGeneratedOnAdd();
            builder.Property(p => p.CreatedDate).ValueGeneratedOnAdd();

            Configure(builder);
        }

        protected abstract void Configure(EntityTypeBuilder<T> builder);
    }
}
