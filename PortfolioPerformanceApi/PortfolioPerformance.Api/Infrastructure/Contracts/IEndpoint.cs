namespace PortfolioPerformance.Api.Infrastructure.Contracts
{
    /// <summary>
    /// Defines a contract for an endpoint in the portfolio performance API.
    /// </summary>
    public interface IEndpoint
    {
        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        public void Configure(IEndpointRouteBuilder app);
    }
}
