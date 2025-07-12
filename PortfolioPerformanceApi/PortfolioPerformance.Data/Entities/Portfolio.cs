namespace PortfolioPerformance.Data.Entities
{
    /// <summary>
    /// Represents a portfolio in the portfolio performance system.
    /// </summary>
    public class Portfolio : BaseEntity
    {
        /// <summary>
        /// Represents the unique identifier for the portfolio.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Represents the name of the portfolio manager or owner.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Represents the list of assets contained within this portfolio.
        /// </summary>
        /// <value>
        /// The assets.
        /// </value>
        public List<Asset> Assets { get; set; } = [];
    }
}
