using FluentAssertions;
using FluentValidation;
using PortfolioPerformance.Api.Features.Portfolio.DTO.Request;
using PortfolioPerformance.Api.Features.Portfolio.Validators;

namespace PortfolioPerformance.Api.Test.Features.Portfolio.Validators
{
    public class UpdatePortfolioRequestValidatorTests
    {
        private readonly UpdatePortfolioRequestValidator _validator = new();

        [Fact]
        public void Should_Throw_When_Name_Is_Null()
        {
            // Arrange
            var model = new UpdatePortfolioRequestDto
            {
                Name = null
            };

            // Act
            Action act = () => _validator.Validate(model);

            // Assert
            var exception = act.Should().Throw<ValidationException>().Which;
            exception.Errors.Should().Contain(e =>
                e.PropertyName == "Name" &&
                e.ErrorMessage == "Name is required.");
        }

        [Fact]
        public void Should_Not_Throw_When_Name_Is_Valid()
        {
            // Arrange
            var model = new UpdatePortfolioRequestDto
            {
                Name = "Fund"
            };

            // Act
            var result = _validator.Validate(model);

            // Assert
            result.IsValid.Should().BeTrue();
        }
    }
}