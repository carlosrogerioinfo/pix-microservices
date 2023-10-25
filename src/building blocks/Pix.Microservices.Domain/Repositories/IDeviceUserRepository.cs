using Pix.Microservices.Domain.Entities;
using Pix.Microservices.Domain.Model;
using Pix.Microservices.Domain.Repositories.Base;

namespace Pix.Microservices.Domain.Repositories
{

    public interface IDeviceUserRepository: IGenericRepository<DeviceUser>
    {
        Task<PagedResponse<DeviceUser, PagedResult>> GetAllAsync(PaginationFilter paginationFilter);
        Task<DeviceUser> GetAsync(Guid id);

    }
}
