namespace PortfolioPerformance.Api.Features.Portfolio.DTO.Request
{
    public record CreatePortfolioRequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
