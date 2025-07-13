using FluentAssertions;
using FluentValidation;
using PortfolioPerformance.Api.Features.Assets.DTO.Request;
using PortfolioPerformance.Api.Features.Assets.Validators;
using PortfolioPerformance.Data.Common;

namespace PortfolioPerformance.Api.Test.Features.Assets.Validators
{
    public class UpdateAssetsRequestValidatorTests
    {
        private readonly UpdateAssetsRequestValidator _validator = new();

        [Fact]
        public void Should_Throw_When_AssetCode_Is_Null()
        {
            var model = new UpdateAssetsRequestDto
            {
                AssetCode = null,
                Type = AssetType.Stock.ToString()
            };

            Action act = () => _validator.Validate(model);

            var exception = act.Should().Throw<ValidationException>().Which;

            exception.Errors.Should().Contain(e =>
                e.PropertyName == "AssetCode" &&
                e.ErrorMessage == "Asset code type is required.");
        }

        [Fact]
        public void Should_Throw_When_Type_Is_Null()
        {
            var model = new UpdateAssetsRequestDto
            {
                AssetCode = "AAPL",
                Type = null
            };

            Action act = () => _validator.Validate(model);

            var exception = act.Should().Throw<ValidationException>().Which;

            exception.Errors.Should().Contain(e =>
                e.PropertyName == "Type" &&
                e.ErrorMessage == "Asset type id is required.");
        }

        [Fact]
        public void Should_Throw_When_Type_Is_Invalid()
        {
            var model = new UpdateAssetsRequestDto
            {
                AssetCode = "AAPL",
                Type = "InvalidType"
            };

            Action act = () => _validator.Validate(model);

            var exception = act.Should().Throw<ValidationException>().Which;

            exception.Errors.Should().Contain(e =>
                e.PropertyName == "Type" &&
                e.ErrorMessage == "Asset type should be one of (Stock/Bond/Crypto)");
        }

        [Fact]
        public void Should_Not_Throw_When_Model_Is_Valid()
        {
            var model = new UpdateAssetsRequestDto
            {
                AssetCode = "AAPL",
                Type = AssetType.Stock.ToString()
            };

            var result = _validator.Validate(model);

            result.IsValid.Should().BeTrue();
        }
    }
}
