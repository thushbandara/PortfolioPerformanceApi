namespace PortfolioPerformance.Api.Infrastructure.Contracts
{
    public interface IEntityMapper
    {
        TDestination Map<TSource, TDestination>(TSource source);
    }
}
