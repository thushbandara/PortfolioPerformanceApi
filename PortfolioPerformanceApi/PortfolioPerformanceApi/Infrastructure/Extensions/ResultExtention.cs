using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PortfolioPerformance.Api.Infrastructure.Extensions
{
    public class ResultExtention
    {
        public class Result
        {
            public bool Success { get; set; }
            public List<string>? Errors { get; set; }
            public HttpStatusCode Status { get; set; }

            protected Result(bool success, List<string>? error, HttpStatusCode status)
            {
                Success = success;
                Errors = error;
                Status = status;
            }
            public static Result Fail(string message, HttpStatusCode code) => new(false, [message], code);
            public static Result Fail(List<string> message, HttpStatusCode code) => new(false, message, code);
        }
    }
}
