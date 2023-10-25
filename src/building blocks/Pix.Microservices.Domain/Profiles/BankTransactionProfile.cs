using AutoMapper;
using Pix.Microservices.Domain.Entities;
using Pix.Microservices.Domain.Http.Response;
using Pix.Core.Lib.Extensions;

namespace Pix.Microservices.Domain.Profiles
{
    public class BankTransactionProfile : Profile
    {
        public BankTransactionProfile()
        {
            CreateMap<BankTransaction, BankTransactionResponse>()
                .ForMember(dest => dest.Bank, opt => opt.MapFrom(src => src.Bank))
                .ForMember(dest => dest.BankAccount, opt => opt.MapFrom(src => src.BankAccount))
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.Device, opt => opt.MapFrom(src => src.Device))
                .ForMember(dest => dest.TransactionId, opt => opt.MapFrom(src => src.TransactionId.ToString().PadLeft(7, '0')))
                .ForMember(dest => dest.StatusCodeType, opt => opt.MapFrom(src => StringExtensionTools.GetDescriptionFromEnum(src.StatusCodeType)))
                .ForMember(dest => dest.StatusCodeType, opt => opt.MapFrom(src => src.StatusCodeType))
                .ReverseMap();
        }
    }
}
