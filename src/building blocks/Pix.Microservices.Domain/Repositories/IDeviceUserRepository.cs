using Pix.Microservices.Domain.Entities;
using Esterdigi.Api.Core.Database.Domain.Model;
using Esterdigi.Api.Core.Database.Domain.Repository.Base;

namespace Pix.Microservices.Domain.Repositories
{

    public interface IDeviceUserRepository: IGenericRepository<DeviceUser>
    {
        Task<PagedResponse<DeviceUser, PagedResult>> GetAllAsync(PaginationFilter paginationFilter);
        Task<DeviceUser> GetAsync(Guid id);

    }
}
