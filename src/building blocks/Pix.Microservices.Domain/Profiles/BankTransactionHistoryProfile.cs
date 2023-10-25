using AutoMapper;
using Pix.Microservices.Domain.Entities;
using Pix.Microservices.Domain.Http.Response;

namespace Pix.Microservices.Domain.Profiles
{
    public class BankTransactionHistoryProfile : Profile
    {
        public BankTransactionHistoryProfile()
        {
            CreateMap<BankTransactionHistory, BankTransactionHistoryResponse>()
                .ForMember(dest => dest.BankTransaction, opt => opt.MapFrom(src => src.BankTransaction))
                .ReverseMap();
        }
    }
}
