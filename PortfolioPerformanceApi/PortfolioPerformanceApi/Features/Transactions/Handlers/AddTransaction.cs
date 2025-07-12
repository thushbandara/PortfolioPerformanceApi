using MediatR;
using PortfolioPerformance.Api.Features.Transactions.DTO;
using PortfolioPerformance.Api.Infrastructure.Contracts;
using PortfolioPerformance.Data.Contracts;

namespace PortfolioPerformance.Api.Features.Transactions.Handlers
{
    /// <summary>
    /// Add a transaction in the portfolio performance system.
    /// </summary>
    public class AddTransaction
    {
        /// <summary>
        /// Endpoint for adding a transaction.
        /// </summary>
        public class EndPoint : IEndpoint
        {
            /// <summary>
            /// Configures the specified application.
            /// </summary>
            /// <param name="app">The application.</param>
            public void Configure(IEndpointRouteBuilder app)
            {
                app.MapPost("/api/transaction", async (CreateTransactionRequestDto request, ISender _sender) =>
                {
                    return Results.Ok(await _sender.Send(new CreateTransactionCommand(request)));
                })
                .WithName("CreateTransaction")
                .WithTags("Transaction");
            }
        }

        /// <summary>
        /// Command to create a transaction.
        /// </summary>
        public record CreateTransactionCommand(CreateTransactionRequestDto Request) : IRequest<Guid>;


        /// <summary>
        /// Handler for creating a transaction.
        /// </summary>
        public class Handler(IPortfolioPerformanceRepository<Data.Entities.Transaction> _repository, IEntityMapper _mapper) : IRequestHandler<CreateTransactionCommand, Guid>
        {
            /// <summary>
            /// Handles a request
            /// </summary>
            /// <param name="request">The request</param>
            /// <param name="cancellationToken">Cancellation token</param>
            /// <returns>
            /// Response from the request
            /// </returns>
            public async Task<Guid> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
            {
                var requestObj = _mapper.Map<CreateTransactionRequestDto, Data.Entities.Transaction>(request.Request);

                await _repository.AddAsync(requestObj);

                return requestObj.Id;
            }
        }
    }
}
