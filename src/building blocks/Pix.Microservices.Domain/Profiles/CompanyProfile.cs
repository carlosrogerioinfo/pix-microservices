using AutoMapper;
using Pix.Microservices.Domain.Entities;
using Pix.Microservices.Domain.Http.Response;

namespace Pix.Microservices.Domain.Profiles
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<Company, CompanyResponse>()
                .ReverseMap();
        }
    }
}
