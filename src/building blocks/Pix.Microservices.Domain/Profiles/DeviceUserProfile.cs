using AutoMapper;
using Pix.Microservices.Domain.Entities;
using Pix.Microservices.Domain.Http.Response;

namespace Pix.Microservices.Domain.Profiles
{
    public class DeviceUserProfile : Profile
    {
        public DeviceUserProfile()
        {
            CreateMap<DeviceUser, DeviceUserResponse>()
                .ForMember(dest => dest.Device, opt => opt.MapFrom(src => src.Device))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ReverseMap();
        }
    }
}
