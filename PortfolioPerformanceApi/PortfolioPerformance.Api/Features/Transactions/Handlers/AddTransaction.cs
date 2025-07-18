﻿using MediatR;
using PortfolioPerformance.Api.Features.Transactions.DTO.Request;
using PortfolioPerformance.Api.Infrastructure.Contracts;
using PortfolioPerformance.Api.Infrastructure.Exceptions;
using PortfolioPerformance.Api.Infrastructure.Extensions;
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
                 .WithValidation<CreateTransactionRequestDto>()
                 .WithOpenApi(operation => new(operation)
                 {
                     Summary = "Create a new transaction",
                     Description = "Creates a new transaction for a specific asset. The transaction can be of type 'Buy' or 'Sell' and must include asset ID, quantity, price, and date. Returns the Id of the newly created transaction."
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
        public class Handler(IPortfolioPerformanceRepository<Data.Entities.Transaction> _transactionRepository,
            IPortfolioPerformanceRepository<Data.Entities.Asset> _assetRepository,
            IEntityMapper _mapper) : IRequestHandler<CreateTransactionCommand, Guid>
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
                _ = await _assetRepository.GetByIdAsync(request.Request.AssetId)
                                               ?? throw new RecordNotFoundException($"Asset with Id {request.Request.AssetId} not found.");

                var requestObj = _mapper.Map<CreateTransactionRequestDto, Data.Entities.Transaction>(request.Request);

                await _transactionRepository.AddAsync(requestObj);

                return requestObj.Id;
            }
        }
    }
}
