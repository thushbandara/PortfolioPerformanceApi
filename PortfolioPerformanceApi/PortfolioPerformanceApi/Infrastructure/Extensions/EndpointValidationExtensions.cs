using PortfolioPerformance.Api.Infrastructure.Filters;

namespace PortfolioPerformance.Api.Infrastructure.Extensions
{
    /// <summary>
    /// Extension methods for endpoint validation.
    /// </summary>
    public static class EndpointValidationExtensions
    {
        /// <summary>
        /// Withes the validation.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder">The builder.</param>
        /// <returns></returns>
        public static RouteHandlerBuilder WithValidation<T>(this RouteHandlerBuilder builder) where T : class
        {
            return builder.AddEndpointFilter<ValidationFilter<T>>();
        }
    }
}
