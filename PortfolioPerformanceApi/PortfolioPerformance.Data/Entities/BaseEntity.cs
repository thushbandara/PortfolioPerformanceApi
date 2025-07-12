namespace PortfolioPerformance.Data.Entities
{
    public class BaseEntity
    {
        /// <summary>
        /// Represents the unique identifier for the entity.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Represents the user who created the entity.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Represents the date when the entity was created.
        /// </summary>
        public DateOnly CreatedDate { get; set; }

        /// <summary>
        /// Represents the user who last updated the entity.
        /// </summary>
        public string? UpdatedBy { get; set; }

        /// <summary>
        /// Represents the date when the entity was last updated.
        /// </summary>
        public DateOnly? UpdatedDate { get; set; }
    }
}
