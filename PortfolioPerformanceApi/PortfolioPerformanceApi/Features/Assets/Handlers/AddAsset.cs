using MediatR;
using PortfolioPerformance.Api.Features.Assets.DTO.Request;
using PortfolioPerformance.Api.Infrastructure.Contracts;
using PortfolioPerformance.Data.Contracts;

namespace PortfolioPerformance.Api.Features.Assets.Handlers
{
    public class AddAsset
    {
        public class EndPoint : IEndpoint
        {
            public void Configure(IEndpointRouteBuilder app)
            {
                app.MapPost("/api/assert", async (AddAssetsRequestDto request, ISender _sender) =>
                {
                    return Results.Ok(await _sender.Send(new AddAssetCommand(request)));
                })
                .WithName("AddAsserts")
                .WithTags("Assert");
            }
        }

        public record AddAssetCommand(AddAssetsRequestDto Request) : IRequest<Guid>;


        public class Handler(IPortfolioPerformanceRepository<Data.Entities.Asset> _assetRepository,
            IPortfolioPerformanceRepository<Data.Entities.Portfolio> _portfolioRepository,
            IEntityMapper _mapper) : IRequestHandler<AddAssetCommand, Guid>
        {
            public async Task<Guid> Handle(AddAssetCommand request, CancellationToken cancellationToken)
            {
                var portfolio = await _portfolioRepository.GetByIdAsync(request.Request.PortfolioId)
                                                ?? throw new KeyNotFoundException($"Portfolio with ID {request.Request.PortfolioId} not found.");


                var requestObj = _mapper.Map<AddAssetsRequestDto, Data.Entities.Asset>(request.Request);

                await _assetRepository.AddAsync(requestObj);

                return requestObj.Id;
            }
        }
    }
}
