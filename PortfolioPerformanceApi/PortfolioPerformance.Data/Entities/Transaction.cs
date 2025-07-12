namespace PortfolioPerformance.Data.Entities
{
    public class Transaction : BaseEntity
    {
        /// <summary>
        /// Represents the unique identifier for the transaction.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Represents the quantity of the asset involved in the transaction.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Represents the price at which the asset was bought or sold in the transaction.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Represents the type of transaction, either "Buy" or "Sell".
        /// </summary>
        public String Type { get; set; } // Buy/Sell

        /// <summary>
        /// Represents the unique identifier for the asset associated with this transaction.
        /// </summary>
        public Guid AssetId { get; set; }

        /// <summary>
        /// Represents the asset that this transaction is associated with.
        /// </summary>
        public Asset Asset { get; set; }
    }
}
