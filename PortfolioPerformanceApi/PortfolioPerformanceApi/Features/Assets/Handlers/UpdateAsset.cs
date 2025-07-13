using MediatR;
using PortfolioPerformance.Api.Features.Assets.DTO.Request;
using PortfolioPerformance.Api.Infrastructure.Contracts;
using PortfolioPerformance.Api.Infrastructure.Exceptions;
using PortfolioPerformance.Data.Contracts;

namespace PortfolioPerformance.Api.Features.Assets.Handlers
{
    /// <summary>
    /// Update an asset in the portfolio performance system.
    /// </summary>
    public class UpdateAsset
    {
        /// <summary>
        /// Endpoint for updating an asset.
        /// </summary>
        public class EndPoint : IEndpoint
        {
            /// <summary>
            /// Configures the specified application.
            /// </summary>
            /// <param name="app">The application.</param>
            public void Configure(IEndpointRouteBuilder app)
            {
                app.MapPatch("/api/asset{id}", async (Guid id, UpdateAssetsRequestDto request, ISender _sender) =>
                {
                    return Results.Ok(await _sender.Send(new UpdateAssetCommand(id, request)));
                })
                .WithName("UpdateAsset")
                .WithTags("Asset");
            }
        }

        /// <summary>
        /// Command to update an asset.
        /// </summary>
        public record UpdateAssetCommand(Guid Id, UpdateAssetsRequestDto Request) : IRequest<Guid>;


        /// <summary>
        /// Handler for updating an asset.
        /// </summary>
        public class Handler(IPortfolioPerformanceRepository<Data.Entities.Asset> _assetRepository,
           IEntityMapper _mapper) : IRequestHandler<UpdateAssetCommand, Guid>
        {
            /// <summary>
            /// Handles a request
            /// </summary>
            /// <param name="request">The request</param>
            /// <param name="cancellationToken">Cancellation token</param>
            /// <returns>
            /// Response from the request
            /// </returns>
            public async Task<Guid> Handle(UpdateAssetCommand request, CancellationToken cancellationToken)
            {
                var asset = await _assetRepository.GetByIdAsync(request.Id)
                                                ?? throw new RecordNotFoundException($"Asset with Id {request.Id} not found.");


                var requestObj = _mapper.Map<UpdateAssetsRequestDto, Data.Entities.Asset>(request.Request);
                requestObj.Id = asset.Id;

                await _assetRepository.Update(requestObj);

                return requestObj.Id;
            }
        }
    }
}
