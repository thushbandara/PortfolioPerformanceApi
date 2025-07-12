namespace PortfolioPerformance.Api.Features.Portfolio.DTO.Request
{
    public record CreatePortfolioDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
