using FluentValidation;
using PortfolioPerformance.Api.Features.Portfolio.DTO.Request;
using PortfolioPerformance.Api.Infrastructure.Common;

namespace PortfolioPerformance.Api.Features.Portfolio.Validators
{
    /// <summary>
    /// Validator for updating a portfolio request in the portfolio performance system.
    /// </summary>
    public class UpdatePortfolioRequestValidator : ModelValidate<UpdatePortfolioRequestDto>
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public UpdatePortfolioRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Name is required.");
        }
    }
}