namespace PortfolioPerformance.Api.Features.Assets.DTO.Request
{
    public record UpdateAssetsRequestDto
    {
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
