using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioPerformance.Data.Entities;

namespace PortfolioPerformance.Data.Configuration
{
    /// <summary>
    /// Base configuration for entities that inherit from <see cref="BaseEntity" />.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        /// <summary>
        /// Configures the entity of type <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        void IEntityTypeConfiguration<T>.Configure(EntityTypeBuilder<T> builder)
        {
            Configure(builder);
        }

        /// <summary>
        /// Configures the entity of type <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        protected abstract void Configure(EntityTypeBuilder<T> builder);
    }
}
