using MediatR;
using PortfolioPerformance.Api.Features.Portfolio.DTO.Response;
using PortfolioPerformance.Api.Infrastructure.Contracts;
using PortfolioPerformance.Api.Infrastructure.Exceptions;
using PortfolioPerformance.Data.Contracts;

namespace PortfolioPerformance.Api.Features.Portfolio.Handlers
{
    /// <summary>
    /// Get a portfolio by its ID in the portfolio performance system.
    /// </summary>
    public class GetPortfolioById
    {
        /// <summary>
        /// Represents the endpoint for getting a portfolio by its ID.
        /// </summary>
        public class EndPoint : IEndpoint
        {
            /// <summary>
            /// Configures the specified application.
            /// </summary>
            /// <param name="app">The application.</param>
            public void Configure(IEndpointRouteBuilder app)
            {
                app.MapGet("/api/portfolio{id}", async (Guid id, ISender _sender) =>
                {
                    return Results.Ok(await _sender.Send(new GetPortfolioQuery(id)));
                })
                 .WithOpenApi(operation => new(operation)
                 {
                     Summary = "Get portfolio by Id",
                     Description = "Retrieves a portfolio's details from the system by its id. Returns portfolio information including name, assets."
                 })
                .WithName("GetPortfolioById")
                .WithTags("Portfolio");
            }
        }

        /// <summary>
        /// Query to get a portfolio by its ID.
        /// </summary>
        public record GetPortfolioQuery(Guid Id) : IRequest<GetPortfolioResponseDto>;


        /// <summary>
        /// Handler for getting a portfolio by its ID.
        /// </summary>
        public class Handler(IPortfolioPerformanceRepository<Data.Entities.Portfolio> _repository, IEntityMapper _mapper) : IRequestHandler<GetPortfolioQuery, GetPortfolioResponseDto>
        {
            /// <summary>
            /// Handles a request
            /// </summary>
            /// <param name="request">The request</param>
            /// <param name="cancellationToken">Cancellation token</param>
            /// <returns>
            /// Response from the request
            /// </returns>
            public async Task<GetPortfolioResponseDto> Handle(GetPortfolioQuery request, CancellationToken cancellationToken)
            {
                var data = await _repository.GetByIdAsync(request.Id)
                                                    ?? throw new RecordNotFoundException($"Portfolio with Id {request.Id} not found.");

                return _mapper.Map<Data.Entities.Portfolio, GetPortfolioResponseDto>(data);
            }
        }
    }
}
