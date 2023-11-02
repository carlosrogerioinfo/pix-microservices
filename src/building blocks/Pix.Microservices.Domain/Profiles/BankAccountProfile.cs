using AutoMapper;
using Pix.Microservices.Domain.Entities;
using Pix.Microservices.Domain.Http.Response;
using Esterdigi.Api.Core.Extensions;
using Esterdigi.Api.Core.Extensions;

namespace Pix.Microservices.Domain.Profiles
{
    public class BankAccountProfile : Profile
    {
        public BankAccountProfile()
        {
            CreateMap<BankAccount, BankAccountResponse>()
                .ForMember(dest => dest.AccountType, opt => opt.MapFrom(src => StringExtensionTools.GetDescriptionFromEnum(src.AccountType)))
                .ForMember(dest => dest.AccountTypeId, opt => opt.MapFrom(src => src.AccountType))
                .ForMember(dest => dest.Bank, opt => opt.MapFrom(src => src.Bank))
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
                .ReverseMap();
        }
    }
}
