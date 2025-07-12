namespace PortfolioPerformance.Data.Entities
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public string CreatedBy { get; set; }
        public DateOnly CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateOnly? UpdatedDate { get; set; }
    }
}
