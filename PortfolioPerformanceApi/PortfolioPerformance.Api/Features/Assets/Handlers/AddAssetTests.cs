using FluentAssertions;
using NSubstitute;
using PortfolioPerformance.Api.Features.Assets.DTO.Request;
using PortfolioPerformance.Api.Features.Assets.Handlers;
using PortfolioPerformance.Api.Infrastructure.Contracts;
using PortfolioPerformance.Data.Contracts;
using PortfolioPerformance.Data.Entities;

namespace PortfolioPerformance.Api.Test.Features.Assets.Handlers
{
    public class AddAssetTests
    {
        [Fact]
        public async Task Handle_WhenPortfolioExists_AddsAssetAndReturnsId()
        {
            // Arrange
            var assetRepo = Substitute.For<IPortfolioPerformanceRepository<Asset>>();
            var portfolioRepo = Substitute.For<IPortfolioPerformanceRepository<Portfolio>>();
            var mapper = Substitute.For<IEntityMapper>();

            var portfolioId = Guid.NewGuid();

            var dto = new AddAssetsRequestDto
            {
                PortfolioId = portfolioId,
                Type = "Stock",
                AssetCode = "AAPL",
            };

            var portfolioEntity = new Portfolio
            {
                Id = portfolioId
            };

            var assetEntity = new Asset
            {
                Id = Guid.NewGuid()
            };

            portfolioRepo.GetByIdAsync(Arg.Any<Guid>()).Returns(portfolioEntity);
            mapper.Map<AddAssetsRequestDto, Asset>(dto).Returns(assetEntity);

            var sut = new AddAsset.Handler(assetRepo, portfolioRepo, mapper);

            // Act
            var result = await sut.Handle(new AddAsset.AddAssetCommand(dto), default);

            // Assert
            result.Should().Be(assetEntity.Id);

            await assetRepo.Received(1).AddAsync(assetEntity);
            await portfolioRepo.Received(1).GetByIdAsync(portfolioId);
        }

        [Fact]
        public async Task Handle_WhenPortfolioMissing_ThrowsAndDoesNotSaveAsset()
        {
            // Arrange
            var assetRepo = Substitute.For<IPortfolioPerformanceRepository<Asset>>();
            var portfolioRepo = Substitute.For<IPortfolioPerformanceRepository<Portfolio>>();
            var mapper = Substitute.For<IEntityMapper>();

            var missingPortfolioId = Guid.NewGuid();

            var dto = new AddAssetsRequestDto
            {
                PortfolioId = missingPortfolioId,
                Type = "Stock",
                AssetCode = "AAPL",
            };

            portfolioRepo.GetByIdAsync(Arg.Any<Guid>()).Returns((Portfolio?)null);
            var sut = new AddAsset.Handler(assetRepo, portfolioRepo, mapper);

            // Act
            Func<Task> act = () => sut.Handle(new AddAsset.AddAssetCommand(dto), default);

            // Assert
            var ex = await act.Should().ThrowAsync<KeyNotFoundException>();
            ex.And.Message.Should().Contain(missingPortfolioId.ToString());

            await assetRepo.DidNotReceive().AddAsync(Arg.Any<Asset>());
        }
    }
}
