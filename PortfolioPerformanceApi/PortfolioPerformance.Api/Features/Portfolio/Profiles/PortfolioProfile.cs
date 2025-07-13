using AutoMapper;
using PortfolioPerformance.Api.Features.Portfolio.DTO.Request;
using PortfolioPerformance.Api.Features.Portfolio.DTO.Response;

namespace PortfolioPerformance.Api.Features.Portfolio.Profiles
{
    /// <summary>
    /// AutoMapper profile for mapping portfolio-related DTOs to entity models and vice versa.
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public class PortfolioProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public PortfolioProfile()
        {
            CreateMap<Data.Entities.Portfolio, GetPortfolioResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

            CreateMap<Data.Entities.Portfolio, CreatePortfolioResponseDto>()
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

            CreateMap<CreatePortfolioRequestDto, Data.Entities.Portfolio>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

            CreateMap<UpdatePortfolioRequestDto, Data.Entities.Portfolio>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
            
        }
    }
}
