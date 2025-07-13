using FluentValidation;
using FluentValidation.Results;

namespace PortfolioPerformance.Api.Infrastructure.Common
{
    /// <summary>
    /// ModelValidate is an abstract class that extends FluentValidation's AbstractValidator.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ModelValidate<T> : AbstractValidator<T>
    {
        /// <summary>
        /// Validates the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public override ValidationResult Validate(ValidationContext<T> context)
        {
            var validation = base.Validate(context);

            if (!validation.IsValid)
            {
                RaiseValidationException(context, validation);
            }

            return validation;
        }
    }
}
