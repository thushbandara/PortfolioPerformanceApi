namespace PortfolioPerformance.Data.Entities
{
    /// <summary>
    /// Base class for all entities in the portfolio performance system.
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Represents the unique identifier for the entity.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Represents the user who created the entity.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public string CreatedBy { get; set; } = "System";

        /// <summary>
        /// Represents the date when the entity was created.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        public DateOnly CreatedDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);

        /// <summary>
        /// Represents the user who last updated the entity.
        /// </summary>
        /// <value>
        /// The updated by.
        /// </value>
        public string? UpdatedBy { get; set; }

        /// <summary>
        /// Represents the date when the entity was last updated.
        /// </summary>
        /// <value>
        /// The updated date.
        /// </value>
        public DateOnly? UpdatedDate { get; set; }
    }
}
