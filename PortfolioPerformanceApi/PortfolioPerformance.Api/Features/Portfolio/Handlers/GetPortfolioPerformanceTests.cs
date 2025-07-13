using FluentAssertions;
using NSubstitute;
using PortfolioPerformance.Api.Features.Portfolio.Handlers;
using PortfolioPerformance.Api.Features.Portfolio.Repositories;
using PortfolioPerformance.Api.Infrastructure.Contracts;
using PortfolioPerformance.Api.Infrastructure.Exceptions;
using PortfolioPerformance.Data.Common;

namespace PortfolioPerformance.Api.Test.Features.Portfolio.Handlers
{
    public class GetPortfolioPerformanceTests
    {
        [Fact]
        public async Task Handle_WhenPortfolioExists_ComputesPerformanceCorrectly()
        {
            // Arrange
            var portfolioRepo = Substitute.For<IPortfolioRepository>();
            var mapper = Substitute.For<IEntityMapper>();
            var portfolioId = Guid.NewGuid();
            var from = new DateTime(2024, 01, 01);
            var to = new DateTime(2024, 12, 31);

            var portfolio = new Data.Entities.Portfolio
            {
                Id = portfolioId,
                Assets =
                [
                    new()
                    {
                        AssetCode = "AAPL",
                        Transactions =
                        [
                            new() {
                                Date = new DateTime(2024, 01, 10),
                                Quantity = 10,
                                Price = 100,
                                Type = nameof(TransactionType.Buy)
                            },
                            new() {
                                Date = new DateTime(2024, 06, 10),
                                Quantity = 5,
                                Price = 120,
                                Type = nameof(TransactionType.Sell)
                            }
                        ]
                    }
                ]
            };

            portfolioRepo.GetByIdAsync(portfolioId).Returns(portfolio);

            var sut = new GetPortfolioPerformance.Handler(portfolioRepo);

            // Act
            var result = await sut.Handle(new GetPortfolioPerformance.GetPortfolioPerformanceQuery(portfolioId, from, to), default);

            // Assert
            result.Total.Should().Be(600);
            result.RealizedGains.Should().Be(100);
            result.UnrealizedGains.Should().Be(100);

            result.AssetAllocations.Should().ContainSingle(a =>
                a.AssetCode == "AAPL" && a.Value == 600);
        }

        [Fact]
        public async Task Handle_WhenPortfolioNotFound_RecordNotFoundException()
        {
            // Arrange
            var portfolioRepo = Substitute.For<IPortfolioRepository>();
            var missingId = Guid.NewGuid();
            var today = DateTime.Now;
            portfolioRepo.GetByIdAsync(missingId).Returns((Data.Entities.Portfolio?)null);
            var sut = new GetPortfolioPerformance.Handler(portfolioRepo);

            // Act
            Func<Task> act = () => sut.Handle(new GetPortfolioPerformance.GetPortfolioPerformanceQuery(missingId, today, today), default);

            // Assert
            var ex = await act.Should().ThrowAsync<RecordNotFoundException>();
            ex.And.Message.Should().Contain(missingId.ToString());
        }
    }
}
