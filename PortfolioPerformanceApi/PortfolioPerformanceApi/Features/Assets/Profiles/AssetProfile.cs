using AutoMapper;
using PortfolioPerformance.Api.Features.Assets.DTO.Request;
using PortfolioPerformance.Data.Common;

namespace PortfolioPerformance.Api.Features.Assets.Profiles
{
    public class AssetProfile : Profile
    {
        public AssetProfile()
        {
            CreateMap<AddAssetsRequestDto, Data.Entities.Asset>()
                .ForMember(dest => dest.PortfolioId, opt => opt.MapFrom(src => src.PortfolioId))
                .ForMember(dest => dest.AssetCode, opt => opt.MapFrom(src => src.AssetCode))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => Enum.Parse<AssetType>(src.Type, true)));

            CreateMap<UpdateAssetsRequestDto, Data.Entities.Asset>()
               .ForMember(dest => dest.AssetCode, opt => opt.MapFrom(src => src.AssetCode))
               .ForMember(dest => dest.Type, opt => opt.MapFrom(src => Enum.Parse<AssetType>(src.Type, true)));
        }
    }
}
