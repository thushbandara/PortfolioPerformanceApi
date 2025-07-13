using AutoMapper;
using PortfolioPerformance.Api.Features.Assets.DTO.Request;
using PortfolioPerformance.Data.Common;

namespace PortfolioPerformance.Api.Features.Assets.Profiles
{
    /// <summary>
    /// AutoMapper profile for mapping asset-related DTOs to entity models and vice versa.
    /// </summary>
    public class AssetProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
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
