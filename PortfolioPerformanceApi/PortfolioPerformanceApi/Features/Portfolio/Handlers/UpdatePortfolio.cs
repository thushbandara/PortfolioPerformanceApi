using MediatR;
using PortfolioPerformance.Api.Features.Portfolio.DTO.Request;
using PortfolioPerformance.Api.Infrastructure.Contracts;
using PortfolioPerformance.Data.Contracts;

namespace PortfolioPerformance.Api.Features.Portfolio.Handlers
{
    public class UpdatePortfolio
    {
        public class EndPoint : IEndpoint
        {
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

        public record UpdatePortfolioCommand(Guid Id, UpdatePortfolioRequestDto Request) : IRequest<Guid>;


        public class Handler(IPortfolioPerformanceRepository<Data.Entities.Portfolio> _repository, IEntityMapper _mapper) : IRequestHandler<UpdatePortfolioCommand, Guid>
        {
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
