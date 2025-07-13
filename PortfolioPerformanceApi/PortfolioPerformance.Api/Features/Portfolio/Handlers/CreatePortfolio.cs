using MediatR;
using PortfolioPerformance.Api.Features.Portfolio.DTO.Request;
using PortfolioPerformance.Api.Infrastructure.Contracts;
using PortfolioPerformance.Api.Infrastructure.Extensions;
using PortfolioPerformance.Data.Contracts;

namespace PortfolioPerformance.Api.Features.Portfolio.Handlers
{
    /// <summary>
    /// Create a portfolio in the portfolio performance system.
    /// </summary>
    public class CreatePortfolio
    {
        /// <summary>
        /// Represents the endpoint for creating a portfolio.
        /// </summary>
        public class EndPoint : IEndpoint
        {
            /// <summary>
            /// Configures the specified application.
            /// </summary>
            /// <param name="app">The application.</param>
            public void Configure(IEndpointRouteBuilder app)
            {
                app.MapPost("/api/portfolio", async (CreatePortfolioRequestDto request, ISender _sender) =>
                {
                    return Results.Ok(await _sender.Send(new CreatePortfolioCommand(request)));
                })
                .WithValidation<CreatePortfolioRequestDto>()
                 .WithOpenApi(operation => new(operation)
                 {
                     Summary = "Create a new portfolio",
                     Description = "Creates a new portfolio in the system. Requires a portfolio name and returns the Id of the newly created portfolio."
                 })
                .WithName("CreatePortfolio")
                .WithTags("Portfolio");
            }
        }

        /// <summary>
        /// Command to create a portfolio.
        /// </summary>
        public record CreatePortfolioCommand(CreatePortfolioRequestDto Request) : IRequest<Guid>;


        /// <summary>
        /// Handler for creating a portfolio.
        /// </summary>
        public class Handler(IPortfolioPerformanceRepository<Data.Entities.Portfolio> _repository, IEntityMapper _mapper) : IRequestHandler<CreatePortfolioCommand, Guid>
        {
            /// <summary>
            /// Handles a request
            /// </summary>
            /// <param name="request">The request</param>
            /// <param name="cancellationToken">Cancellation token</param>
            /// <returns>
            /// Response from the request
            /// </returns>
            public async Task<Guid> Handle(CreatePortfolioCommand request, CancellationToken cancellationToken)
            {
                var requestObj = _mapper.Map<CreatePortfolioRequestDto, Data.Entities.Portfolio>(request.Request);

                await _repository.AddAsync(requestObj);

                return requestObj.Id;
            }
        }
    }
}
