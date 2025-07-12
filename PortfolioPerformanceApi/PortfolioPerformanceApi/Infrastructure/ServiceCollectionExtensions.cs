using Microsoft.EntityFrameworkCore;
using PortfolioPerformance.Data;
using System.Reflection;

namespace PortfolioPerformance.Api.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddDbContext<PortfolioPerformanceContext>(opt => opt.UseInMemoryDatabase("PortfolioDb"));


            return services;
        }
    }
}
