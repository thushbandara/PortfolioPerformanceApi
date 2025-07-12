namespace PortfolioPerformance.Api.Features.Portfolio.DTO.Response
{
    /// <summary>
    /// Represents the response DTO for portfolio performance details.
    /// </summary>
    public record GetPortfolioPerformanceResponseDto
    {
        /// <summary>
        /// Gets or sets the total value.
        /// </summary>
        /// <value>
        /// The total value.
        /// </value>
        public decimal TotalValue { get; set; }
        /// <summary>
        /// Gets or sets the asset allocations.
        /// </summary>
        /// <value>
        /// The asset allocations.
        /// </value>
        public List<AssetAllocationDto> AssetAllocations { get; set; } = [];
        /// <summary>
        /// Gets or sets the realized gains.
        /// </summary>
        /// <value>
        /// The realized gains.
        /// </value>
        public decimal RealizedGains { get; set; }
        /// <summary>
        /// Gets or sets the unrealized gains.
        /// </summary>
        /// <value>
        /// The unrealized gains.
        /// </value>
        public decimal UnrealizedGains { get; set; }
    }

    /// <summary>
    /// Represents the asset allocation details in the portfolio performance response DTO.
    /// </summary>
    public class AssetAllocationDto
    {
        /// <summary>
        /// Gets or sets the asset code.
        /// </summary>
        /// <value>
        /// The asset code.
        /// </value>
        public string AssetCode { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public decimal Value { get; set; }
    }
}
