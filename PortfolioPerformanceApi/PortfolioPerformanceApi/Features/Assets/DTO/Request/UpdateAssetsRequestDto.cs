namespace PortfolioPerformance.Api.Features.Assets.DTO.Request
{
    /// <summary>
    /// Represents a request to update an asset in the portfolio performance system.
    /// </summary>
    public record UpdateAssetsRequestDto
    {
        /// <summary>
        /// Represents the unique identifier for the asset.
        /// </summary>
        /// <value>
        /// The asset code.
        /// </value>
        /// <example>MSFT</example>
        public string AssetCode { get; set; }

        /// <summary>
        /// Represents the type of the asset.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        /// <example>Stock</example>
        /// <example>Bonds</example>
        public string Type { get; set; }
    }
}
