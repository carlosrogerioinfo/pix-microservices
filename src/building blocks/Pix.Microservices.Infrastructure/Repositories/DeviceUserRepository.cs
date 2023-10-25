using Microsoft.EntityFrameworkCore;
using Pix.Microservices.Domain.Entities;
using Pix.Microservices.Domain.Extention;
using Pix.Microservices.Domain.Model;
using Pix.Microservices.Domain.Repositories;
using Pix.Microservices.Infrastructure.Contexts;
using Pix.Microservices.Infrastructure.Repositories.Base;

namespace Pix.Microservices.Infrastructure.Repositories
{
    public class DeviceUserRepository : GenericRepository<DeviceUser>, IDeviceUserRepository
    {
        public DeviceUserRepository(PixDataContext context) : base(context)
        {

        }

        public async Task<PagedResponse<DeviceUser, PagedResult>> GetAllAsync(PaginationFilter paginationFilter)
        {
            var queryable = await _dbSet.GetPagedAsync(paginationFilter.Page, paginationFilter.PageSize);

            var entity = await _dbSet
                .Include(x => x.Device)
                .Include(x => x.User)
                .AsNoTrackingWithIdentityResolution()
                .Skip((paginationFilter.Page - 1) * paginationFilter.PageSize)
                .Take(paginationFilter.PageSize)
                .ToListAsync();

            return new PagedResponse<DeviceUser, PagedResult>(entity, queryable);
        }

        public async Task<DeviceUser> GetAsync(Guid id)
        {
            return await _dbSet
                .Include(x => x.Device)
                .Include(x => x.User)
                .Where(x => x.Id == id)
                .AsNoTrackingWithIdentityResolution()
                .FirstOrDefaultAsync();
        }
    }
}
