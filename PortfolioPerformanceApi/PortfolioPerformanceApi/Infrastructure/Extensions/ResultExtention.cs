using System.Net;

namespace PortfolioPerformance.Api.Infrastructure.Extensions
{
    /// <summary>
    /// ResultExtention is a class that provides extension methods for handling results in the application.
    /// </summary>
    public class ResultExtention
    {
        /// <summary>
        /// Represents the result of an operation, encapsulating success status, errors, and HTTP status code.
        /// </summary>
        public class Result
        {
            /// <summary>
            /// Gets or sets a value indicating whether this <see cref="Result"/> is success.
            /// </summary>
            /// <value>
            ///   <c>true</c> if success; otherwise, <c>false</c>.
            /// </value>
            public bool Success { get; set; }
            /// <summary>
            /// Gets or sets the errors.
            /// </summary>
            /// <value>
            /// The errors.
            /// </value>
            public List<string>? Errors { get; set; }
            /// <summary>
            /// Gets or sets the status.
            /// </summary>
            /// <value>
            /// The status.
            /// </value>
            public HttpStatusCode Status { get; set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="Result"/> class.
            /// </summary>
            /// <param name="success">if set to <c>true</c> [success].</param>
            /// <param name="error">The error.</param>
            /// <param name="status">The status.</param>
            protected Result(bool success, List<string>? error, HttpStatusCode status)
            {
                Success = success;
                Errors = error;
                Status = status;
            }
            /// <summary>
            /// Fails the specified message.
            /// </summary>
            /// <param name="message">The message.</param>
            /// <param name="code">The code.</param>
            /// <returns></returns>
            public static Result Fail(string message, HttpStatusCode code) => new(false, [message], code);
            /// <summary>
            /// Fails the specified message.
            /// </summary>
            /// <param name="message">The message.</param>
            /// <param name="code">The code.</param>
            /// <returns></returns>
            public static Result Fail(List<string> message, HttpStatusCode code) => new(false, message, code);
        }
    }
}
