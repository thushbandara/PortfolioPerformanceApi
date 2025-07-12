namespace PortfolioPerformance.Data.Entities
{
    public class Portfolio : BaseEntity
    {
        /// <summary>
        /// Represents the unique identifier for the portfolio.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Represents the name of the portfolio manager or owner.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Represents the list of assets contained within this portfolio.
        /// </summary>
        public List<Asset> Assets { get; set; } = [];
    }
}
