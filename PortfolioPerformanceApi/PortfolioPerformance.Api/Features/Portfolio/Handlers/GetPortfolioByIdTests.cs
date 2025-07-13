using FluentAssertions;
using NSubstitute;
using PortfolioPerformance.Api.Features.Portfolio.DTO.Response;
using PortfolioPerformance.Api.Features.Portfolio.Handlers;
using PortfolioPerformance.Api.Infrastructure.Contracts;
using PortfolioPerformance.Data.Contracts;

namespace PortfolioPerformance.Api.Test.Features.Portfolio.Handlers
{
    public class GetPortfolioByIdTests
    {
        [Fact]
        public async Task Handle_WhenPortfolioExists_ReturnsMappedDto()
        {
            // Arrange
            var portfolioRepo = Substitute.For<IPortfolioPerformanceRepository<Data.Entities.Portfolio>>();
            var mapper = Substitute.For<IEntityMapper>();
            var portfolioId = Guid.NewGuid();
            var entity = new Data.Entities.Portfolio
            {
                Id = portfolioId
            };

            var expectedDto = new GetPortfolioResponseDto
            {
                Id = portfolioId,
                Name = "Sample Portfolio"
            };

            portfolioRepo.GetByIdAsync(portfolioId).Returns(entity);
            mapper.Map<Data.Entities.Portfolio, GetPortfolioResponseDto>(entity).Returns(expectedDto);

            var sut = new GetPortfolioById.Handler(portfolioRepo, mapper);

            // Act
            var result = await sut.Handle(new GetPortfolioById.GetPortfolioQuery(portfolioId), default);

            // Assert
            result.Should().BeEquivalentTo(expectedDto);
            await portfolioRepo.Received(1).GetByIdAsync(portfolioId);
        }

        [Fact]
        public async Task Handle_WhenPortfolioNotFound_ThrowsKeyNotFoundException()
        {
            // Arrange
            var portfolioRepo = Substitute.For<IPortfolioPerformanceRepository<Data.Entities.Portfolio>>();
            var mapper = Substitute.For<IEntityMapper>();
            var missingId = Guid.NewGuid();
            portfolioRepo.GetByIdAsync(missingId).Returns((Data.Entities.Portfolio?)null);
            var sut = new GetPortfolioById.Handler(portfolioRepo, mapper);

            // Act
            Func<Task> act = () => sut.Handle(new GetPortfolioById.GetPortfolioQuery(missingId), default);

            // Assert
            var ex = await act.Should().ThrowAsync<KeyNotFoundException>();
            ex.And.Message.Should().Contain(missingId.ToString());

            await portfolioRepo.Received(1).GetByIdAsync(missingId);
        }
    }
}
