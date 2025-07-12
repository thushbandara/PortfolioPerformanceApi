using PortfolioPerformance.Data.Common;

namespace PortfolioPerformance.Api.Features.Assets.DTO.Request
{
    public record AddAssetsRequestDto
    {
        /// <summary>
        /// Represents the unique identifier for the portfolio to which the asset belongs.
        /// </summary>
        public Guid PortfolioId { get; set; }

        /// <summary>
        /// Represents the unique identifier for the asset.
        /// </summary>
        /// <example>MSFT</example>
        public string AssetCode { get; set; }

        /// <summary>
        /// Represents the type of the asset.
        /// </summary>
        /// <example>Stock</example>
        /// <example>Bonds</example>
        public string Type { get; set; }
    }
}
