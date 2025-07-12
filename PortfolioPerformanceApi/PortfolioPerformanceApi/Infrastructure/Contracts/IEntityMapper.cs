﻿namespace PortfolioPerformance.Api.Infrastructure.Contracts
{
    /// <summary>
    /// Interface for entity mapping between different types.
    /// </summary>
    public interface IEntityMapper
    {
        /// <summary>
        /// Maps the specified source.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TDestination">The type of the destination.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        TDestination Map<TSource, TDestination>(TSource source);
    }
}
