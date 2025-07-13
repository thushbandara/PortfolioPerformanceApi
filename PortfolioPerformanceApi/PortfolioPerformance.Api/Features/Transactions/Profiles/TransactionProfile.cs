using AutoMapper;
using PortfolioPerformance.Api.Features.Transactions.DTO.Request;
using PortfolioPerformance.Data.Common;

namespace PortfolioPerformance.Api.Features.Transactions.Profiles
{
    /// <summary>
    /// 
    /// </summary>
    public class TransactionProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public TransactionProfile()
        {
            CreateMap<CreateTransactionRequestDto, Data.Entities.Transaction>()
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.AssetId, opt => opt.MapFrom(src => src.AssetId))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => Enum.Parse<TransactionType>(src.Type, true)));
        }
    }
}
