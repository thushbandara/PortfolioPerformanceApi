using MediatR;
using PortfolioPerformance.Api.Infrastructure.Contracts;
using PortfolioPerformance.Data.Contracts;

namespace PortfolioPerformance.Api.Features.Assets.Handlers
{
    /// <summary>
    /// Delete an asset in the portfolio performance system.
    /// </summary>
    public class DeleteAsset
    {
        /// <summary>
        /// Endpoint for deleting an asset.
        /// </summary>
        public class EndPoint : IEndpoint
        {
            /// <summary>
            /// Configures the specified application.
            /// </summary>
            /// <param name="app">The application.</param>
            public void Configure(IEndpointRouteBuilder app)
            {
                app.MapDelete("/api/assert{id}", async (Guid id, ISender _sender) =>
                {
                    return Results.Ok(await _sender.Send(new DeleteAssetCommand(id)));
                })
                .WithName("DeleteAssert")
                .WithTags("Assert");
            }
        }

        /// <summary>
        /// Command to delete an asset.
        /// </summary>
        public record DeleteAssetCommand(Guid Id) : IRequest<Guid>;


        /// <summary>
        /// Handler for deleting an asset.
        /// </summary>
        public class Handler(IPortfolioPerformanceRepository<Data.Entities.Asset> _assetRepository) : IRequestHandler<DeleteAssetCommand, Guid>
        {
            /// <summary>
            /// Handles a request
            /// </summary>
            /// <param name="request">The request</param>
            /// <param name="cancellationToken">Cancellation token</param>
            /// <returns>
            /// Response from the request
            /// </returns>
            public async Task<Guid> Handle(DeleteAssetCommand request, CancellationToken cancellationToken)
            {
                var asset = await _assetRepository.GetByIdAsync(request.Id)
                                                ?? throw new KeyNotFoundException($"Asset with ID {request.Id} not found.");

                await _assetRepository.Remove(asset);

                return asset.Id;
            }
        }
    }
}
