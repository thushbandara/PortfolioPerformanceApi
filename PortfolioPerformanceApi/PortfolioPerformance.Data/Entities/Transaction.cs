namespace PortfolioPerformance.Data.Entities
{
    public class Transaction : BaseEntity
    {
        public DateTime Date { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public String Type { get; set; } // Buy/Sell
        public int AssetId { get; set; }
        public Asset Asset { get; set; }
    }
}
