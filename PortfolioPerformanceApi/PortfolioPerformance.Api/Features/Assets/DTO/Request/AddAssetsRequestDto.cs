namespace PortfolioPerformance.Api.Features.Assets.DTO.Request
{
    /// <summary>
    /// Represents a request to add assets to a portfolio in the portfolio performance system.
    /// </summary>
    public record AddAssetsRequestDto
    {
        /// <summary>
        /// Represents the unique identifier for the portfolio to which the asset belongs.
        /// </summary>
        /// <value>
        /// The portfolio identifier.
        /// </value>
        public Guid PortfolioId { get; init; }

        /// <summary>
        /// Represents the unique identifier for the asset.
        /// </summary>
        /// <value>
        /// The asset code.
        /// </value>
        /// <example>MSFT</example>
        public string AssetCode { get; init; }

        /// <summary>
        /// Represents the type of the asset.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        /// <example>Stock</example>
        /// <example>Bonds</example>
        public string Type { get; init; }
    }
}
