using MediatR;
using PortfolioPerformance.Api.Features.Portfolio.DTO.Response;
using PortfolioPerformance.Api.Infrastructure.Contracts;
using PortfolioPerformance.Data.Contracts;

namespace PortfolioPerformance.Api.Features.Portfolio.Handlers
{
    public class GetPortfolioById
    {
        public class EndPoint : IEndpoint
        {
            public void Configure(IEndpointRouteBuilder app)
            {
                app.MapGet("/api/portfolio{id}", async (Guid id, ISender _sender) =>
                {
                    return Results.Ok(await _sender.Send(new GetPortfolioQuery(id)));
                })
                .WithName("GetPortfolioById")
                .WithTags("Portfolio");
            }
        }

        public record GetPortfolioQuery(Guid Id) : IRequest<GetPortfolioResponseDto>;


        public class Handler(IPortfolioPerformanceRepository<Data.Entities.Portfolio> _repository, IEntityMapper _mapper) : IRequestHandler<GetPortfolioQuery, GetPortfolioResponseDto>
        {
            public async Task<GetPortfolioResponseDto> Handle(GetPortfolioQuery request, CancellationToken cancellationToken)
            {
                var data = await _repository.GetByIdAsync(request.Id)
                                                    ?? throw new KeyNotFoundException($"Portfolio with ID {request.Id} not found.");

                return _mapper.Map<Data.Entities.Portfolio, GetPortfolioResponseDto>(data);
            }
        }
    }
}
