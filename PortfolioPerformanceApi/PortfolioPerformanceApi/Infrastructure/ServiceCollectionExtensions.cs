using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PortfolioPerformance.Api.Features.Portfolio.Repositories;
using PortfolioPerformance.Api.Infrastructure.Common;
using PortfolioPerformance.Api.Infrastructure.Contracts;
using PortfolioPerformance.Data;
using PortfolioPerformance.Data.Contracts;
using PortfolioPerformance.Data.Repositories;
using System.Reflection;

namespace PortfolioPerformance.Api.Infrastructure
{
    /// <summary>
    /// Extension methods for registering services in the service collection.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddDbContext<PortfolioPerformanceContext>(opt => opt.UseInMemoryDatabase("PortfolioDb"));
            services.AddScoped(typeof(IPortfolioPerformanceRepository<>), typeof(PortfolioPerformanceRepository<>));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddValidatorsFromAssemblyContaining(typeof(ModelValidate<>));

            services.AddScoped<IEntityMapper, EntityMapper>();
            services.AddScoped<IPortfolioRepository, PortfolioRepository>();

            return services;
        }
    }
}
