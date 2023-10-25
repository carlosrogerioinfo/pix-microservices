using AutoMapper;
using Pix.Microservices.Domain.Entities;
using Pix.Microservices.Domain.Http.Response;

namespace Pix.Microservices.Domain.Profiles
{
    public class BankProfile : Profile
    {
        public BankProfile()
        {
            CreateMap<Bank, BankResponse>()
                .ReverseMap();
        }
    }
}
