using FluentAssertions;
using NSubstitute;
using PortfolioPerformance.Api.Features.Assets.DTO.Request;
using PortfolioPerformance.Api.Features.Assets.Handlers;
using PortfolioPerformance.Api.Infrastructure.Contracts;
using PortfolioPerformance.Data.Contracts;
using PortfolioPerformance.Data.Entities;

namespace PortfolioPerformance.Api.Test.Features.Assets.Handlers
{
    public class UpdateAssetTests
    {
        [Fact]
        public async Task Handle_WhenAssetExists_UpdatesAndReturnsId()
        {
            // Arrange
            var assetRepo = Substitute.For<IPortfolioPerformanceRepository<Asset>>();
            var portfolioRepo = Substitute.For<IPortfolioPerformanceRepository<Portfolio>>();
            var mapper = Substitute.For<IEntityMapper>();
            var assetId = Guid.NewGuid();
            var dto = new UpdateAssetsRequestDto
            {
                Type = "Stock",
                AssetCode = "AAPL",
            };

            var existingAsset = new Asset
            {
                Id = assetId
            };

            var mappedAsset = new Asset
            {
                Id = Guid.NewGuid()
            };

            assetRepo.GetByIdAsync(Arg.Any<Guid>()).Returns(existingAsset);
            mapper.Map<UpdateAssetsRequestDto, Asset>(dto).Returns(mappedAsset);

            var sut = new UpdateAsset.Handler(assetRepo, mapper);

            // Act
            var result = await sut.Handle(new UpdateAsset.UpdateAssetCommand(assetId, dto), default);

            // Assert
            result.Should().Be(assetId);
            mappedAsset.Id.Should().Be(assetId);

            await assetRepo.Received(1).Update(mappedAsset);
        }

        [Fact]
        public async Task Handle_WhenAssetMissing_ThrowsAndDoesNotUpdate()
        {
            // Arrange
            var assetRepo = Substitute.For<IPortfolioPerformanceRepository<Asset>>();
            var portfolioRepo = Substitute.For<IPortfolioPerformanceRepository<Portfolio>>();
            var mapper = Substitute.For<IEntityMapper>();
            var missingId = Guid.NewGuid();
            var dto = new UpdateAssetsRequestDto
            {
                Type = "Stock",
                AssetCode = "AAPL",
            };

            assetRepo.GetByIdAsync(Arg.Any<Guid>()).Returns((Asset?)null);
            var sut = new UpdateAsset.Handler(assetRepo, mapper);

            // Act
            Func<Task> act = () => sut.Handle(new UpdateAsset.UpdateAssetCommand(missingId, dto), default);

            // Assert
            var ex = await act.Should().ThrowAsync<KeyNotFoundException>();
            ex.And.Message.Should().Contain(missingId.ToString());

            await assetRepo.DidNotReceive().Update(Arg.Any<Asset>());
        }
    }
}
