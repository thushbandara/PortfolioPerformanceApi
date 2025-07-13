using FluentValidation;
using Newtonsoft.Json;
using PortfolioPerformance.Api.Infrastructure.Exceptions;
using System.Net;
using static PortfolioPerformance.Api.Infrastructure.Extensions.ResultExtention;

namespace PortfolioPerformance.Api.Infrastructure.Middleware
{
    /// <summary>
    /// Middleware to handle errors globally in the application.
    /// </summary>
    public class ErrorHandlerMiddleware(RequestDelegate next)
    {
        /// <summary>
        /// The next
        /// </summary>
        private readonly RequestDelegate _next = next;

        /// <summary>
        /// Invokes the specified HTTP context.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                var response = httpContext.Response;
                response.ContentType = "application/json";

                response.StatusCode = ex switch
                {
                    ValidationException => (int)HttpStatusCode.BadRequest,
                    RecordNotFoundException => (int)HttpStatusCode.NotFound,
                    _ => (int)HttpStatusCode.InternalServerError
                };

                var errorModel = ex switch
                {
                    ValidationException exceptions => Result.Fail([.. exceptions.Errors.Select(a => a.ErrorMessage)], HttpStatusCode.BadRequest),
                    RecordNotFoundException => Result.Fail(ex.Message, HttpStatusCode.NotFound),
                    _ => Result.Fail("Error occurred while processing the request.", HttpStatusCode.InternalServerError)
                };

                var result = JsonConvert.SerializeObject(errorModel);

                await response.WriteAsync(result);
            }
        }
    }
}
