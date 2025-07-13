using AutoMapper;
using PortfolioPerformance.Api.Infrastructure.Contracts;
using System.Reflection;

namespace PortfolioPerformance.Api.Infrastructure.Common
{
    /// <summary>
    /// EntityMapper is a class that implements the IEntityMapper interface.
    /// </summary>
    /// <seealso cref="PortfolioPerformance.Api.Infrastructure.Contracts.IEntityMapper" />

    public class EntityMapper : IEntityMapper
    {
        /// <summary>
        /// The mapper
        /// </summary>
        private IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityMapper" /> class.
        /// </summary>
        public EntityMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(Assembly.GetExecutingAssembly());
            });

            _mapper = config.CreateMapper();
        }

        /// <summary>
        /// Maps the specified source.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TDestination">The type of the destination.</typeparam>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return _mapper.Map<TSource, TDestination>(source);
        }
    }
}
