namespace PortfolioPerformance.Api.Features.Portfolio.DTO.Request
{
    public record UpdatePortfolioRequestDto
    {
        public string Name { get; init; }
        public string Description { get; init; }
    }
}
