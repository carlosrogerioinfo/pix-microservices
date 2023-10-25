using AutoMapper;
using Pix.Microservices.Domain.Entities;
using Pix.Microservices.Domain.Http.Response;

namespace Pix.Microservices.Domain.Profiles
{
    public class DeviceProfile : Profile
    {
        public DeviceProfile()
        {
            CreateMap<Device, DeviceResponse>()
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
                .ReverseMap();
        }
    }
}
