using FluentValidation;
using PortfolioPerformance.Api.Features.Transactions.DTO.Request;
using PortfolioPerformance.Api.Infrastructure.Common;
using PortfolioPerformance.Data.Common;

namespace PortfolioPerformance.Api.Features.Transactions.Validators
{
    /// <summary>
    /// Validator for creating a transaction request.
    /// </summary>
    public class CreateTransactionRequestValidator : ModelValidate<CreateTransactionRequestDto>
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public CreateTransactionRequestValidator()
        {
            RuleFor(x => x.AssetId)
                .NotNull().WithMessage("Asset id is required.");

            RuleFor(x => x.Price)
                .NotNull().WithMessage("Price is required.")
                .Must(value => value > 0).WithMessage("Price must be greater than zero.");

            RuleFor(x => x.Quantity)
                .NotNull().WithMessage("Quantity id is required.")
                .Must(value => value > 0).WithMessage("Quantity must be greater than zero.");

            RuleFor(x => x.Type)
              .NotNull().WithMessage("Transaction type id is required.")
              .Must(value => Enum.TryParse<TransactionType>(value, true, out _))
              .WithMessage("Transaction type should be one of (Buy/Sell)");
        }
    }
}