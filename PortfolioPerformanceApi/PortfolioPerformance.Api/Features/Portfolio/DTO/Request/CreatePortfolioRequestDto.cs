namespace PortfolioPerformance.Api.Features.Portfolio.DTO.Request
{
    /// <summary>
    /// Data Transfer Object for creating a portfolio request.
    /// </summary>
    public record CreatePortfolioRequestDto
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; init; }
        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; init; }
    }
}
