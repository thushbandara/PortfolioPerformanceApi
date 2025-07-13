using FluentValidation;

namespace PortfolioPerformance.Api.Infrastructure.Filters
{
    /// <summary>
    /// ValidationFilter is a class that implements IEndpointFilter to validate models.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ValidationFilter<T> : IEndpointFilter where T : class
    {
        /// <summary>
        /// and the next filter to call in the pipeline.
        /// </summary>
        /// <returns>
        /// An awaitable result of calling the handler and apply
        /// any modifications made by filters in the pipeline.
        /// </returns>
        public async ValueTask<object?> InvokeAsync(
            EndpointFilterInvocationContext context,
            EndpointFilterDelegate next)
        {
            var model = context.Arguments.OfType<T>().FirstOrDefault();
            if (model is null)
                return Results.BadRequest("Invalid input");

            var validator = context.HttpContext.RequestServices.GetService<IValidator<T>>();
            if (validator is null)
                return Results.BadRequest("Validator not found");

            var result = await validator.ValidateAsync(model);

            if (!result.IsValid)
            {
                var errors = result.ToDictionary();
                return Results.ValidationProblem(errors);
            }

            return await next(context);
        }
    }
}
