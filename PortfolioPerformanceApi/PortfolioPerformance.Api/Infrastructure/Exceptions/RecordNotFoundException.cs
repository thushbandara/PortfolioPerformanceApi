namespace PortfolioPerformance.Api.Infrastructure.Exceptions
{
    /// <summary>
    /// Exception thrown when a record is not found in the database or data source.
    /// </summary>
    public class RecordNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public RecordNotFoundException() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public RecordNotFoundException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the  class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public RecordNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
