using FluentAssertions;
using NSubstitute;
using PortfolioPerformance.Api.Features.Transactions.DTO.Request;
using PortfolioPerformance.Api.Features.Transactions.Handlers;
using PortfolioPerformance.Api.Infrastructure.Contracts;
using PortfolioPerformance.Data.Contracts;

namespace PortfolioPerformance.Api.Test.Features.Transactions.Handlers
{
    public class AddTransactionTests
    {
        [Fact]
        public async Task Handle_WhenAssetExists_AddsTransactionAndReturnsId()
        {
            // Arrange
            var transactionRepo = Substitute.For<IPortfolioPerformanceRepository<Data.Entities.Transaction>>();
            var assetRepo = Substitute.For<IPortfolioPerformanceRepository<Data.Entities.Asset>>();
            var mapper = Substitute.For<IEntityMapper>();
            var assetId = Guid.NewGuid();
            var dto = new CreateTransactionRequestDto
            {
                AssetId = assetId,
                Quantity = 10,
                Price = 100.0m,
                Type = "Buy"
            };

            var existingAsset = new Data.Entities.Asset { Id = assetId };
            var mappedTransaction = new Data.Entities.Transaction { Id = Guid.NewGuid() };

            assetRepo.GetByIdAsync(assetId).Returns(existingAsset);
            mapper.Map<CreateTransactionRequestDto, Data.Entities.Transaction>(dto).Returns(mappedTransaction);

            var sut = new AddTransaction.Handler(transactionRepo, assetRepo, mapper);

            // Act
            var result = await sut.Handle(new AddTransaction.CreateTransactionCommand(dto), default);

            // Assert
            result.Should().Be(mappedTransaction.Id);

            await assetRepo.Received(1).GetByIdAsync(assetId);
            await transactionRepo.Received(1).AddAsync(mappedTransaction);
        }

        [Fact]
        public async Task Handle_WhenAssetMissing_ThrowsKeyNotFoundException()
        {
            // Arrange
            var transactionRepo = Substitute.For<IPortfolioPerformanceRepository<Data.Entities.Transaction>>();
            var assetRepo = Substitute.For<IPortfolioPerformanceRepository<Data.Entities.Asset>>();
            var mapper = Substitute.For<IEntityMapper>();
            var missingId = Guid.NewGuid();
            var dto = new CreateTransactionRequestDto
            {
                AssetId = missingId,
                Quantity = 10,
                Price = 100.0m,
                Type = "Sell"
            };


            assetRepo.GetByIdAsync(missingId).Returns((Data.Entities.Asset?)null);
            var sut = new AddTransaction.Handler(transactionRepo, assetRepo, mapper);

            // Act
            Func<Task> act = () => sut.Handle(new AddTransaction.CreateTransactionCommand(dto), default);

            // Assert
            var ex = await act.Should().ThrowAsync<KeyNotFoundException>();
            ex.And.Message.Should().Contain(missingId.ToString());

            await transactionRepo.DidNotReceive().AddAsync(Arg.Any<Data.Entities.Transaction>());
        }
    }
}