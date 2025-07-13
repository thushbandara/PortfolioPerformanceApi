using FluentAssertions;
using NSubstitute;
using PortfolioPerformance.Api.Features.Portfolio.DTO.Request;
using PortfolioPerformance.Api.Features.Portfolio.Handlers;
using PortfolioPerformance.Api.Infrastructure.Contracts;
using PortfolioPerformance.Data.Contracts;

namespace PortfolioPerformance.Api.Test.Features.Portfolio.Handlers
{
    public class UpdatePortfolioTests
    {
        [Fact]
        public async Task Handle_WhenPortfolioExists_UpdatesAndReturnsId()
        {
            // Arrange
            var portfolioRepo = Substitute.For<IPortfolioPerformanceRepository<Data.Entities.Portfolio>>();
            var mapper = Substitute.For<IEntityMapper>();
            var id = Guid.NewGuid();
            var dto = new UpdatePortfolioRequestDto
            {
                Name = "New Portfolio Name",
            };

            var existingEntity = new Data.Entities.Portfolio
            {
                Id = id
            };

            var mappedEntity = new Data.Entities.Portfolio
            {
                Id = id
            };

            portfolioRepo.GetByIdAsync(Arg.Any<Guid>()).Returns(existingEntity);
            mapper.Map<UpdatePortfolioRequestDto, Data.Entities.Portfolio>(dto).Returns(mappedEntity);

            var sut = new UpdatePortfolio.Handler(portfolioRepo, mapper);

            // Act
            var result = await sut.Handle(new UpdatePortfolio.UpdatePortfolioCommand(id, dto), default);

            // Assert
            result.Should().Be(id);
            mappedEntity.Id.Should().Be(id);

            await portfolioRepo.Received(1).Update(mappedEntity);
        }

        [Fact]
        public async Task Handle_WhenPortfolioNotFound_ThrowsKeyNotFound()
        {
            // Arrange
            var portfolioRepo = Substitute.For<IPortfolioPerformanceRepository<Data.Entities.Portfolio>>();
            var mapper = Substitute.For<IEntityMapper>();
            var id = Guid.NewGuid();

            var dto = new UpdatePortfolioRequestDto
            {
                Name = "New Portfolio Name",
            };

            portfolioRepo.GetByIdAsync(Arg.Any<Guid>()).Returns((Data.Entities.Portfolio?)null);
            var sut = new UpdatePortfolio.Handler(portfolioRepo, mapper);

            // Act
            Func<Task> act = () => sut.Handle(new UpdatePortfolio.UpdatePortfolioCommand(id, dto), default);

            // Assert
            var ex = await act.Should().ThrowAsync<KeyNotFoundException>();
            ex.And.Message.Should().Contain(id.ToString());

            await portfolioRepo.DidNotReceive().Update(Arg.Any<Data.Entities.Portfolio>());
        }
    }
}
