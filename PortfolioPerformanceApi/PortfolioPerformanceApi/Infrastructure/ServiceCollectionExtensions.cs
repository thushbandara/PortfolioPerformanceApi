using Microsoft.EntityFrameworkCore;
using PortfolioPerformance.Data;
using PortfolioPerformance.Data.Contracts;
using PortfolioPerformance.Data.Repositories;
using System.Reflection;

namespace PortfolioPerformance.Api.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddDbContext<PortfolioPerformanceContext>(opt => opt.UseInMemoryDatabase("PortfolioDb"));
            services.AddScoped(typeof(IPortfolioPerformanceRepository<>), typeof(PortfolioPerformanceRepository<>));

            return services;
        }
    }
}
