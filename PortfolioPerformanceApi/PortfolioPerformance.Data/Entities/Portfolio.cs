namespace PortfolioPerformance.Data.Entities
{
    public class Portfolio : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Asset> Assets { get; set; } = [];
    }
}
