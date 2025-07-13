using FluentAssertions;
using FluentValidation;
using PortfolioPerformance.Api.Features.Transactions.DTO.Request;
using PortfolioPerformance.Api.Features.Transactions.Validators;
using PortfolioPerformance.Data.Common;

namespace PortfolioPerformance.Api.Test.Features.Transactions.Validators
{
    public class CreateTransactionRequestValidatorTests
    {
        private readonly CreateTransactionRequestValidator _validator = new();

        [Fact]
        public void Should_Throw_When_AssetId_Is_Empty()
        {
            var model = new CreateTransactionRequestDto
            {
                AssetId = Guid.Empty,
                Price = 100,
                Quantity = 10,
                Type = TransactionType.Buy.ToString()
            };

            Action act = () => _validator.Validate(model);

            var exception = act.Should().Throw<ValidationException>().Which;
            exception.Errors.Should().Contain(e => e.PropertyName == "AssetId" && e.ErrorMessage == "Invalid asset id format");
        }

        [Fact]
        public void Should_Throw_When_Price_Is_Zero()
        {
            var model = new CreateTransactionRequestDto
            {
                AssetId = Guid.NewGuid(),
                Price = 0,
                Quantity = 10,
                Type = TransactionType.Buy.ToString()
            };

            Action act = () => _validator.Validate(model);

            var exception = act.Should().Throw<ValidationException>().Which;
            exception.Errors.Should().Contain(e => e.PropertyName == "Price" && e.ErrorMessage == "Price must be greater than zero.");
        }

        [Fact]
        public void Should_Throw_When_Quantity_Is_Negative()
        {
            var model = new CreateTransactionRequestDto
            {
                AssetId = Guid.NewGuid(),
                Price = 100,
                Quantity = -5,
                Type = TransactionType.Sell.ToString()
            };

            Action act = () => _validator.Validate(model);

            var exception = act.Should().Throw<ValidationException>().Which;
            exception.Errors.Should().Contain(e => e.PropertyName == "Quantity" && e.ErrorMessage == "Quantity must be greater than zero.");
        }

        [Fact]
        public void Should_Throw_When_Type_Is_Invalid()
        {
            var model = new CreateTransactionRequestDto
            {
                AssetId = Guid.NewGuid(),
                Price = 100,
                Quantity = 10,
                Type = "InvalidType"
            };

            Action act = () => _validator.Validate(model);

            var exception = act.Should().Throw<ValidationException>().Which;
            exception.Errors.Should().Contain(e => e.PropertyName == "Type" && e.ErrorMessage == "Transaction type should be one of (Buy/Sell)");
        }

        [Fact]
        public void Should_Not_Throw_When_Model_Is_Valid()
        {
            var model = new CreateTransactionRequestDto
            {
                AssetId = Guid.NewGuid(),
                Price = 150.50m,
                Quantity = 25,
                Type = TransactionType.Buy.ToString()
            };

            var result = _validator.Validate(model);

            result.IsValid.Should().BeTrue();
        }
    }
}