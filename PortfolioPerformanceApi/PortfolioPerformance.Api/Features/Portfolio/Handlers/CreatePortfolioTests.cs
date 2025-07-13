using FluentAssertions;
using NSubstitute;
using PortfolioPerformance.Api.Features.Portfolio.DTO.Request;
using PortfolioPerformance.Api.Features.Portfolio.Handlers;
using PortfolioPerformance.Api.Infrastructure.Contracts;
using PortfolioPerformance.Data.Contracts;

namespace PortfolioPerformance.Api.Test.Features.Portfolio.Handlers
{
    public class CreatePortfolioTests
    {
        [Fact]
        public async Task Handle_WhenCalled_MapsAndAddsPortfolio_ReturnsId()
        {
            // Arrange
            var portfolioRepo = Substitute.For<IPortfolioPerformanceRepository<Data.Entities.Portfolio>>();
            var mapper = Substitute.For<IEntityMapper>();

            var dto = new CreatePortfolioRequestDto
            {
                Name = "Portfolio Name",
                Description = "Portfolio description.",
            };

            var mappedEntity = new Data.Entities.Portfolio
            {
                Id = Guid.NewGuid()
            };

            mapper.Map<CreatePortfolioRequestDto, Data.Entities.Portfolio>(dto).Returns(mappedEntity);

            var sut = new CreatePortfolio.Handler(portfolioRepo, mapper);

            // Act
            var result = await sut.Handle(new CreatePortfolio.CreatePortfolioCommand(dto), default);

            // Assert
            result.Should().Be(mappedEntity.Id);
            await portfolioRepo.Received(1).AddAsync(mappedEntity);
        }
    }
}
