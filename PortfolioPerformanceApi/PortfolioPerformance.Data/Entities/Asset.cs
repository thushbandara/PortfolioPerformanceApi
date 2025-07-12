namespace PortfolioPerformance.Data.Entities
{
    /// <summary>
    /// Represents an asset in the portfolio performance system.
    /// </summary>
    public class Asset : BaseEntity
    {
        /// <summary>
        /// Represents the unique identifier for the asset.
        /// </summary>
        /// <value>
        /// The asset code.
        /// </value>
        public string AssetCode { get; set; }

        /// <summary>
        /// Represents the name of the asset.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type { get; set; }

        /// <summary>
        /// Represents the unique identifier for the portfolio to which this asset belongs.
        /// </summary>
        /// <value>
        /// The portfolio identifier.
        /// </value>
        public Guid PortfolioId { get; set; }

        /// <summary>
        /// Represents the portfolio that contains this asset.
        /// </summary>
        /// <value>
        /// The portfolio.
        /// </value>
        public Portfolio Portfolio { get; set; }

        /// <summary>
        /// Represents the list of transactions associated with this asset.
        /// </summary>
        /// <value>
        /// The transactions.
        /// </value>
        public List<Transaction> Transactions { get; set; } = [];
    }
}
