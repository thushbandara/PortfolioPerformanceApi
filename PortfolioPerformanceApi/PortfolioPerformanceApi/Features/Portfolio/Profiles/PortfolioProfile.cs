using AutoMapper;

namespace PortfolioPerformance.Api.Features.Portfolio.Profiles
{
    public class PortfolioProfile : Profile
    {
        public PortfolioProfile()
        {
            CreateMap<Data.Entities.Portfolio, DTO.Response.GetPortfolioResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

            CreateMap<Data.Entities.Portfolio, DTO.Response.CreatePortfolioResponse>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

            CreateMap<DTO.Request.CreatePortfolioDto, Data.Entities.Portfolio>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
        }
    }
}
