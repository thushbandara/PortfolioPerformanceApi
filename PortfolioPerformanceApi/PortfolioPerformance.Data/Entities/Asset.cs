namespace PortfolioPerformance.Data.Entities
{
    public class Asset : BaseEntity
    {
        public string Symbol { get; set; }
        public string Type { get; set; }
        public int PortfolioId { get; set; }
        public Portfolio Portfolio { get; set; }
        public List<Transaction> Transactions { get; set; } = [];
    }
}
