using Pix.Microservices.Domain.Entities;
using Pix.Microservices.Domain.Repositories;
using Pix.Microservices.Infrastructure.Contexts;
using Core.Integration.Infrastructure.Repository.Base;

namespace Pix.Microservices.Infrastructure.Repositories
{
    public class DeviceRepository : GenericRepository<Device>, IDeviceRepository
    {
        public DeviceRepository(PixDataContext context) : base(context)
        {

        }

    }
}
