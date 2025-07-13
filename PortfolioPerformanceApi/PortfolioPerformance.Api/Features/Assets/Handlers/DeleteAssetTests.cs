using FluentAssertions;
using NSubstitute;
using PortfolioPerformance.Api.Features.Assets.Handlers;
using PortfolioPerformance.Api.Infrastructure.Exceptions;
using PortfolioPerformance.Data.Contracts;
using PortfolioPerformance.Data.Entities;

namespace PortfolioPerformance.Api.Test.Features.Assets.Handlers
{
    public class DeleteAssetTests
    {
        [Fact]
        public async Task Handle_WhenAssetExists_RemovesAndReturnsId()
        {
            // Arrange
            var assetRepo = Substitute.For<IPortfolioPerformanceRepository<Asset>>();
            var assetId = Guid.NewGuid();
            var assetEntity = new Asset { Id = assetId };

            assetRepo.GetByIdAsync(assetId).Returns(assetEntity);

            var sut = new DeleteAsset.Handler(assetRepo);

            // Act
            var result = await sut.Handle(new DeleteAsset.DeleteAssetCommand(assetId), default);

            // Assert
            result.Should().Be(assetId);
            await assetRepo.Received(1).Remove(assetEntity);
            await assetRepo.Received(1).GetByIdAsync(assetId);
        }

        [Fact]
        public async Task Handle_WhenAssetMissing_ThrowsAndDoesNotRemove()
        {
            // Arrange
            var assetRepo = Substitute.For<IPortfolioPerformanceRepository<Asset>>();
            var missingId = Guid.NewGuid();
            assetRepo.GetByIdAsync(missingId).Returns((Asset?)null);

            var sut = new DeleteAsset.Handler(assetRepo);

            // Act
            Func<Task> act = () => sut.Handle(new DeleteAsset.DeleteAssetCommand(missingId), default);

            // Assert
            var ex = await act.Should().ThrowAsync<RecordNotFoundException>();
            ex.And.Message.Should().Contain(missingId.ToString());

            await assetRepo.DidNotReceive().Remove(Arg.Any<Asset>());
        }
    }
}
