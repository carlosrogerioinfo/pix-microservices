using Pix.Microservices.Domain.Entities;
using Core.Integration.Domain.Model;
using Core.Integration.Domain.Repository.Base;

namespace Pix.Microservices.Domain.Repositories
{

    public interface IDeviceUserRepository: IGenericRepository<DeviceUser>
    {
        Task<PagedResponse<DeviceUser, PagedResult>> GetAllAsync(PaginationFilter paginationFilter);
        Task<DeviceUser> GetAsync(Guid id);

    }
}
