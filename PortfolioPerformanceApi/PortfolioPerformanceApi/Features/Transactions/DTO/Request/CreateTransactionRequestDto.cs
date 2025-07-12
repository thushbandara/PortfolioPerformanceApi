namespace PortfolioPerformance.Api.Features.Transactions.DTO.Request
{
    /// <summary>
    /// Data Transfer Object for creating a transaction request.
    /// </summary>
    public record CreateTransactionRequestDto
    {
        /// <summary>
        /// Gets or sets the asset identifier.
        /// </summary>
        /// <value>
        /// The asset identifier.
        /// </value>
        public Guid AssetId { get; init; }
        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>
        /// The quantity.
        /// </value>
        public decimal Quantity { get; init; }
        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        public decimal Price { get; init; }
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        /// <example>Buy</example>
        /// <example>Sell</example>
        public string Type { get; init; }
    }
}
