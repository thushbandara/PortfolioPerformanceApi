using FluentValidation;
using PortfolioPerformance.Api.Features.Portfolio.DTO.Request;
using PortfolioPerformance.Api.Infrastructure.Common;

namespace PortfolioPerformance.Api.Features.Portfolio.Validators
{
    /// <summary>
    /// Validator for creating a portfolio request in the portfolio performance system.
    /// </summary>
    public class CreatePortfolioRequestValidator : ModelValidate<CreatePortfolioRequestDto>
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public CreatePortfolioRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage("Name is required.");
        }
    }
}