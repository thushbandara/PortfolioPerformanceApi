using MediatR;
using PortfolioPerformance.Api.Features.Assets.DTO.Request;
using PortfolioPerformance.Api.Infrastructure.Contracts;
using PortfolioPerformance.Api.Infrastructure.Exceptions;
using PortfolioPerformance.Api.Infrastructure.Extensions;
using PortfolioPerformance.Data.Contracts;

namespace PortfolioPerformance.Api.Features.Assets.Handlers
{
    /// <summary>
    /// Add an asset in the portfolio performance system.
    /// </summary>
    public class AddAsset
    {
        /// <summary>
        /// Endpoint for adding an asset.
        /// </summary>
        public class EndPoint : IEndpoint
        {
            /// <summary>
            /// Configures the specified application.
            /// </summary>
            /// <param name="app">The application.</param>
            public void Configure(IEndpointRouteBuilder app)
            {
                app.MapPost("/api/asset", async (AddAssetsRequestDto request, ISender _sender) =>
                {
                    return Results.Ok(await _sender.Send(new AddAssetCommand(request)));
                })
                .WithValidation<AddAssetsRequestDto>()
                .WithOpenApi(operation => new(operation)
                {
                    Summary = "Create a new asset",
                    Description = "Adds a new asset to an existing portfolio. Requires details such as asset code, type, and associated portfolio Id. Returns the newly created asset Id."
                })
                .WithName("AddAsset")
                .WithTags("Asset");
            }
        }

        /// <summary>
        /// Command to add an asset.
        /// </summary>
        public record AddAssetCommand(AddAssetsRequestDto Request) : IRequest<Guid>;


        /// <summary>
        /// Handler for adding an asset.
        /// </summary>
        public class Handler(IPortfolioPerformanceRepository<Data.Entities.Asset> _assetRepository,
            IPortfolioPerformanceRepository<Data.Entities.Portfolio> _portfolioRepository,
            IEntityMapper _mapper) : IRequestHandler<AddAssetCommand, Guid>
        {
            /// <summary>
            /// Handles a request
            /// </summary>
            /// <param name="request">The request</param>
            /// <param name="cancellationToken">Cancellation token</param>
            /// <returns>
            /// Response from the request
            /// </returns>
            public async Task<Guid> Handle(AddAssetCommand request, CancellationToken cancellationToken)
            {
                _ = await _portfolioRepository.GetByIdAsync(request.Request.PortfolioId)
                                                ?? throw new RecordNotFoundException($"Portfolio with Id {request.Request.PortfolioId} not found.");


                var requestObj = _mapper.Map<AddAssetsRequestDto, Data.Entities.Asset>(request.Request);

                await _assetRepository.AddAsync(requestObj);

                return requestObj.Id;
            }
        }
    }
}
