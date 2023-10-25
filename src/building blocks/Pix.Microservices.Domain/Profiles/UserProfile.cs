using AutoMapper;
using Pix.Microservices.Domain.Entities;
using Pix.Microservices.Domain.Http.Response;
using Pix.Core.Lib.Extensions;

namespace Pix.Microservices.Domain.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserResponse>()
                .ForMember(dest => dest.UserTypeId, opt => opt.MapFrom(src => src.UserType))
                .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => StringExtensionTools.GetDescriptionFromEnum(src.UserType)))
                .ReverseMap();
        }
    }
}
