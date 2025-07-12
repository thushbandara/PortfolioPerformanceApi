namespace PortfolioPerformance.Api.Features.Portfolio.DTO.Response
{
    public record CreatePortfolioResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
