using AutoMapper;
using PortfolioPerformance.Data.Contracts;
using System.Reflection;

namespace PortfolioPerformance.Data.Common
{
    public class EntityMapper : IEntityMapper
    {
        private IMapper _mapper;

        public EntityMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(Assembly.GetExecutingAssembly());
            });
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return _mapper.Map<TSource, TDestination>(source);
        }
    }
}
