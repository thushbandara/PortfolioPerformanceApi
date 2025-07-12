using MediatR;
using PortfolioPerformance.Api.Features.Portfolio.DTO.Request;
using PortfolioPerformance.Api.Infrastructure.Contracts;
using PortfolioPerformance.Data.Contracts;

namespace PortfolioPerformance.Api.Features.Portfolio.Handlers
{
    public class CreatePortfolio
    {
        public class EndPoint : IEndpoint
        {
            public void Configure(IEndpointRouteBuilder app)
            {
                app.MapPost("/api/portfolio", async (CreatePortfolioRequestDto request, ISender _sender) =>
                {
                    return Results.Ok(await _sender.Send(new CreatePortfolioCommand(request)));
                })
                .WithName("CreatePortfolio")
                .WithTags("Portfolio");
            }
        }

        public record CreatePortfolioCommand(CreatePortfolioRequestDto Request) : IRequest<Guid>;


        public class Handler(IPortfolioPerformanceRepository<Data.Entities.Portfolio> _repository, IEntityMapper _mapper) : IRequestHandler<CreatePortfolioCommand, Guid>
        {
            public async Task<Guid> Handle(CreatePortfolioCommand request, CancellationToken cancellationToken)
            {
                var requestObj = _mapper.Map<CreatePortfolioRequestDto, Data.Entities.Portfolio>(request.Request);

                await _repository.AddAsync(requestObj);

                return requestObj.Id;
            }
        }
    }
}
