using PortfolioPerformance.Data.Common;

namespace PortfolioPerformance.Data.Entities
{
    /// <summary>
    /// Represents a transaction in the portfolio performance system.
    /// </summary>
    public class Transaction : BaseEntity
    {
        /// <summary>
        /// Represents the unique identifier for the transaction.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public DateTime Date { get; set; }

        /// <summary>
        /// Represents the quantity of the asset involved in the transaction.
        /// </summary>
        /// <value>
        /// The quantity.
        /// </value>
        public decimal Quantity { get; set; }

        /// <summary>
        /// Represents the price at which the asset was bought or sold in the transaction.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        public decimal Price { get; set; }

        /// <summary>
        /// Represents the type of transaction, either "Buy" or "Sell".
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type { get; set; }

        /// <summary>
        /// Represents the unique identifier for the asset associated with this transaction.
        /// </summary>
        /// <value>
        /// The asset identifier.
        /// </value>
        public Guid AssetId { get; set; }

        /// <summary>
        /// Represents the asset that this transaction is associated with.
        /// </summary>
        /// <value>
        /// The asset.
        /// </value>
        public Asset Asset { get; set; }
    }
}
