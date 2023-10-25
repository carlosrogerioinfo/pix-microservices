using AutoMapper;
using Pix.Microservices.Domain.Entities;
using Pix.Microservices.Domain.Http.Response;

namespace Pix.Microservices.Domain.Profiles
{
    public class CompanyUserProfile : Profile
    {
        public CompanyUserProfile()
        {
            CreateMap<CompanyUser, CompanyUserResponse>()
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ReverseMap();
        }
    }
}
