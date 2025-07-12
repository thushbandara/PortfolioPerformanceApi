using PortfolioPerformance.Data.Common;

namespace PortfolioPerformance.Api.Features.Assets.DTO.Request
{
    public record AddAssetsRequestDto
    {
        public Guid PortfolioId { get; set; }
        public string AssetCode { get; set; }
        public AssetType Type { get; set; } 
    }
}
