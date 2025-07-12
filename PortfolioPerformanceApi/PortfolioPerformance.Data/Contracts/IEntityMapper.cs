namespace PortfolioPerformance.Data.Contracts
{
    public interface IEntityMapper
    {
        TDestination Map<TSource, TDestination>(TSource source);
    }
}
