using Pix.Microservices.Domain.Entities;
using Core.Repository.Domain.Model;
using Core.Repository.Domain.Repository.Base;

namespace Pix.Microservices.Domain.Repositories
{

    public interface IDeviceUserRepository: IGenericRepository<DeviceUser>
    {
        Task<PagedResponse<DeviceUser, PagedResult>> GetAllAsync(PaginationFilter paginationFilter);
        Task<DeviceUser> GetAsync(Guid id);

    }
}
