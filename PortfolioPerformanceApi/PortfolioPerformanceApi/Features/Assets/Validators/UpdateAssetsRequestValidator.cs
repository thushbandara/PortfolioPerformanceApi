using FluentValidation;
using PortfolioPerformance.Api.Features.Assets.DTO.Request;
using PortfolioPerformance.Api.Infrastructure.Common;
using PortfolioPerformance.Data.Common;

namespace PortfolioPerformance.Api.Features.Assets.Validators
{
    /// <summary>
    /// Validator for adding assets request DTO.
    /// </summary>
    public class UpdateAssetsRequestValidator : ModelValidate<UpdateAssetsRequestDto>
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public UpdateAssetsRequestValidator()
        {
            RuleFor(x => x.AssetCode)
                .NotNull().WithMessage("Asset code type is required.");

            RuleFor(x => x.Type)
              .NotNull().WithMessage("Asset type id is required.")
              .Must(value => Enum.TryParse<AssetType>(value, true, out _))
              .WithMessage("Asset type should be one of (Stock/Bond/Crypto)");
        }
    }
}
