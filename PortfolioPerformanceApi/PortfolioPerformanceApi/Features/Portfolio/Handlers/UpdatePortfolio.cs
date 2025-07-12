using MediatR;
using PortfolioPerformance.Api.Features.Portfolio.DTO.Request;
using PortfolioPerformance.Api.Infrastructure.Contracts;
using PortfolioPerformance.Data.Contracts;

namespace PortfolioPerformance.Api.Features.Portfolio.Handlers
{
    /// <summary>
    /// Update a portfolio in the portfolio performance system.
    /// </summary>
    public class UpdatePortfolio
    {
        /// <summary>
        /// Represents the endpoint for updating a portfolio by its ID.
        /// </summary>
        public class EndPoint : IEndpoint
        {
            /// <summary>
            /// Configures the specified application.
            /// </summary>
            /// <param name="app">The application.</param>
            public void Configure(IEndpointRouteBuilder app)
            {
                app.MapPatch("/api/portfolio{id}", async (Guid id, UpdatePortfolioRequestDto request, ISender _sender) =>
                {
                    return Results.Ok(await _sender.Send(new UpdatePortfolioCommand(id, request)));
                })
                .WithName("UpdatePortfolio")
                .WithTags("Portfolio");
            }
        }

        /// <summary>
        /// Command to update a portfolio by its ID.
        /// </summary>
        public record UpdatePortfolioCommand(Guid Id, UpdatePortfolioRequestDto Request) : IRequest<Guid>;


        /// <summary>
        /// Handler for updating a portfolio by its ID.
        /// </summary>
        public class Handler(IPortfolioPerformanceRepository<Data.Entities.Portfolio> _repository, IEntityMapper _mapper) : IRequestHandler<UpdatePortfolioCommand, Guid>
        {
            /// <summary>
            /// Handles a request
            /// </summary>
            /// <param name="request">The request</param>
            /// <param name="cancellationToken">Cancellation token</param>
            /// <returns>
            /// Response from the request
            /// </returns>
            public async Task<Guid> Handle(UpdatePortfolioCommand request, CancellationToken cancellationToken)
            {
                var data = await _repository.GetByIdAsync(request.Id)
                                                 ?? throw new KeyNotFoundException($"Portfolio with ID {request.Id} not found.");


                var requestObj = _mapper.Map<UpdatePortfolioRequestDto, Data.Entities.Portfolio>(request.Request);
                requestObj.Id = data.Id; 

                await _repository.Update(requestObj);

                return requestObj.Id;
            }
        }
    }
}
