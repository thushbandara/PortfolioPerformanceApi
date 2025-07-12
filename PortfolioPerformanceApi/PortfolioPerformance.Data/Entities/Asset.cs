namespace PortfolioPerformance.Data.Entities
{
    public class Asset : BaseEntity
    {
        /// <summary>
        /// Represents the unique identifier for the asset.
        /// </summary>
        public string Symbol { get; set; }

        /// <summary>
        /// Represents the name of the asset.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Represents the unique identifier for the portfolio to which this asset belongs.
        /// </summary>
        public int PortfolioId { get; set; }

        /// <summary>
        /// Represents the portfolio that contains this asset.
        /// </summary>
        public Portfolio Portfolio { get; set; }

        /// <summary>
        /// Represents the list of transactions associated with this asset.
        /// </summary>
        public List<Transaction> Transactions { get; set; } = [];
    }
}
