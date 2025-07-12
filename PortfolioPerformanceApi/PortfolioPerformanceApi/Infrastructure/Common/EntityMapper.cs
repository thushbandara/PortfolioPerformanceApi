using AutoMapper;
using PortfolioPerformance.Api.Infrastructure.Contracts;
using System.Reflection;

namespace PortfolioPerformance.Api.Infrastructure.Common
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

            _mapper = config.CreateMapper();
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return _mapper.Map<TSource, TDestination>(source);
        }
    }
}
