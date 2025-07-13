using FluentAssertions;
using FluentValidation;
using PortfolioPerformance.Api.Features.Assets.DTO.Request;
using PortfolioPerformance.Api.Features.Assets.Validators;
using PortfolioPerformance.Data.Common;

namespace PortfolioPerformance.Api.Test.Features.Assets.Validators
{
    public class AddAssetsRequestValidatorTests
    {
        private readonly AddAssetsRequestValidator _validator = new();

        [Fact]
        public void Should_Have_Error_When_AssetCode_Is_Null()
        {
            var model = new AddAssetsRequestDto
            {
                AssetCode = null,
                PortfolioId = Guid.NewGuid(),
                Type = AssetType.Stock.ToString()
            };

            Action act = () => _validator.Validate(model);

            // Assert
            var exception = act.Should().Throw<ValidationException>().Which;

            exception.Errors.Should()
                .Contain(e => e.PropertyName == "AssetCode" && e.ErrorMessage == "Asset code type is required.");

        }

        [Fact]
        public void Should_Have_Error_When_PortfolioId_Is_Empty()
        {
            var model = new AddAssetsRequestDto
            {
                AssetCode = "AAPL",
                PortfolioId = Guid.Empty,
                Type = AssetType.Stock.ToString()
            };

            Action act = () => _validator.Validate(model);

            // Assert
            var exception = act.Should().Throw<ValidationException>().Which;

            exception.Errors.Should()
                .Contain(e => e.PropertyName == "PortfolioId" && e.ErrorMessage == "Invalid portfolio id format");
        }

        [Fact]
        public void Should_Have_Error_When_Type_Is_Invalid()
        {
            var model = new AddAssetsRequestDto
            {
                AssetCode = "AAPL",
                PortfolioId = Guid.NewGuid(),
                Type = "InvalidType"
            };

            Action act = () => _validator.Validate(model);

            // Assert
            var exception = act.Should().Throw<ValidationException>().Which;

            exception.Errors.Should()
                .Contain(e => e.PropertyName == "Type" && e.ErrorMessage == "Asset type should be one of (Stock/Bond/Crypto)");
        }

        [Fact]
        public void Should_Not_Have_Error_For_Valid_Model()
        {
            var model = new AddAssetsRequestDto
            {
                AssetCode = "AAPL",
                PortfolioId = Guid.NewGuid(),
                Type = AssetType.Stock.ToString()
            };

            var result = _validator.Validate(model);
            result.Errors.Count.Should().Be(0);
        }
    }
}
