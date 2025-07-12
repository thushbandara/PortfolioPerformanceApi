using MediatR;
using PortfolioPerformance.Api.Features.Portfolio.DTO.Request;
using PortfolioPerformance.Api.Features.Portfolio.DTO.Response;
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
                app.MapPost("/api/portfolio", async (CreatePortfolioDto request, ISender _sender) =>
                {
                    return Results.Ok(await _sender.Send(new CreatePortfolioCommand(request)));
                })
                .WithName("CreatePortfolio")
                .WithTags("Portfolio");
            }
        }

        public record CreatePortfolioCommand(CreatePortfolioDto Request) : IRequest<CreatePortfolioResponse>;


        public class Handler(IPortfolioPerformanceRepository<Data.Entities.Portfolio> _repository, IEntityMapper _mapper) : IRequestHandler<CreatePortfolioCommand, CreatePortfolioResponse>
        {
            public async Task<CreatePortfolioResponse> Handle(CreatePortfolioCommand request, CancellationToken cancellationToken)
            {
                var requestObj = _mapper.Map<CreatePortfolioDto, Data.Entities.Portfolio>(request.Request);

                await _repository.AddAsync(requestObj);

                return _mapper.Map<Data.Entities.Portfolio, CreatePortfolioResponse>(requestObj);
            }
        }
    }
}
