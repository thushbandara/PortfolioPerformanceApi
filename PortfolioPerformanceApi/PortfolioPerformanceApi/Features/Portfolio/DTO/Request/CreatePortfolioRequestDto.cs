namespace PortfolioPerformance.Api.Features.Portfolio.DTO.Request
{
    public record CreatePortfolioRequestDto
    {
        public string Name { get; init; }
        public string Description { get; init; }
    }
}
