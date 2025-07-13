using MediatR;
using PortfolioPerformance.Api.Features.Portfolio.DTO.Response;
using PortfolioPerformance.Api.Features.Portfolio.Repositories;
using PortfolioPerformance.Api.Infrastructure.Contracts;
using PortfolioPerformance.Api.Infrastructure.Exceptions;
using PortfolioPerformance.Data.Common;

namespace PortfolioPerformance.Api.Features.Portfolio.Handlers
{
    /// <summary>
    /// Get the performance of a portfolio in the portfolio performance system.
    /// </summary>
    public class GetPortfolioPerformance
    {
        /// <summary>
        /// Represents the endpoint for getting portfolio performance details.
        /// </summary>
        public class EndPoint : IEndpoint
        {
            /// <summary>
            /// Configures the specified application.
            /// </summary>
            /// <param name="app">The application.</param>
            public void Configure(IEndpointRouteBuilder app)
            {
                app.MapGet("/api/portfolio_perfomance", async (Guid portfolioId, DateTime from, DateTime to, ISender _sender) =>
                {
                    return Results.Ok(await _sender.Send(new GetPortfolioPerformanceQuery(portfolioId, from, to)));
                })
                 .WithOpenApi(operation => new(operation)
                 {
                     Summary = "Get portfolio performance",
                     Description = "Retrieves the performance of a specific portfolio within the given date range. Returns total value, asset allocations, realized gains, and unrealized gains."
                 })
                .WithName("PortfolioPerfomance")
                .WithTags("Portfolio");
            }
        }

        /// <summary>
        /// Query to get the performance of a portfolio.
        /// </summary>
        public record GetPortfolioPerformanceQuery(Guid PortfolioId, DateTime From, DateTime To) : IRequest<GetPortfolioPerformanceResponseDto>;

        /// <summary>
        /// Handler for getting the performance of a portfolio.
        /// </summary>
        public class Handler(IPortfolioRepository _repository) : IRequestHandler<GetPortfolioPerformanceQuery, GetPortfolioPerformanceResponseDto>
        {
            /// <summary>
            /// Handles a request
            /// </summary>
            /// <param name="request">The request</param>
            /// <param name="cancellationToken">Cancellation token</param>
            /// <returns>
            /// Response from the request
            /// </returns>
            public async Task<GetPortfolioPerformanceResponseDto> Handle(GetPortfolioPerformanceQuery request, CancellationToken cancellationToken)
            {
                var portfolio = await _repository.GetByIdAsync(request.PortfolioId)
                                                    ?? throw new RecordNotFoundException($"Portfolio with Id {request.PortfolioId} not found.");

                var totalValue = 0m;
                var realizedGains = 0m;
                var unrealizedGains = 0m;
                var allocations = new Dictionary<string, decimal>();

                foreach (var asset in portfolio.Assets)
                {
                    var transactions = asset.Transactions.Where(t => t.Date >= request.From && t.Date <= request.To).ToList();
                    var buys = transactions.Where(t => t.Type == nameof(TransactionType.Buy)).ToList();
                    var sells = transactions.Where(t => t.Type == nameof(TransactionType.Sell)).ToList();

                    var totalBuyQty = buys.Sum(b => b.Quantity);
                    var totalSellQty = sells.Sum(s => s.Quantity);
                    var netQty = totalBuyQty - totalSellQty;
                    var avgBuyPrice = totalBuyQty > 0 ? buys.Sum(b => b.Quantity * b.Price) / totalBuyQty : 0m;
                    var latestPrice = transactions.OrderByDescending(t => t.Date).FirstOrDefault()?.Price ?? 0m;

                    var assetValue = netQty * latestPrice;
                    totalValue += assetValue;
                    allocations[asset.AssetCode] = assetValue;

                    realizedGains += sells.Sum(s => s.Quantity * (s.Price - avgBuyPrice));
                    unrealizedGains += netQty * (latestPrice - avgBuyPrice);
                }

                return new GetPortfolioPerformanceResponseDto
                {
                    Total = totalValue,
                    AssetAllocations = [.. allocations.Select(a => new AssetAllocationDto
                    {
                        AssetCode = a.Key,
                        Value = a.Value
                    })],
                    RealizedGains = realizedGains,
                    UnrealizedGains = unrealizedGains
                };
            }
        }
    }
}
