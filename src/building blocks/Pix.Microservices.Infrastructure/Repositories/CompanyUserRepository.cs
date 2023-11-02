using Esterdigi.Api.Core.Database.Domain.Extention;
using Esterdigi.Api.Core.Database.Domain.Model;
using Esterdigi.Api.Core.Database.Infrastructure.Repository.Base;
using Microsoft.EntityFrameworkCore;
using Pix.Microservices.Domain.Entities;
using Pix.Microservices.Domain.Repositories;
using Pix.Microservices.Infrastructure.Contexts;

namespace Pix.Microservices.Infrastructure.Repositories
{
    public class CompanyUserRepository : GenericRepository<CompanyUser>, ICompanyUserRepository
    {
        public CompanyUserRepository(PixDataContext context) : base(context)
        {

        }

        public async Task<PagedResponse<CompanyUser, PagedResult>> GetAllAsync(PaginationFilter paginationFilter)
        {
            var queryable = await _dbSet.GetPagedAsync(paginationFilter.Page, paginationFilter.PageSize);

            var entity = await _dbSet
                .Include(x => x.Company)
                .Include(x => x.User)
                .AsNoTrackingWithIdentityResolution()
                .Skip((paginationFilter.Page - 1) * paginationFilter.PageSize)
                .Take(paginationFilter.PageSize)
                .ToListAsync();

            return new PagedResponse<CompanyUser, PagedResult>(entity, queryable);
        }

        public async Task<CompanyUser> GetAsync(Guid id)
        {
            return await _dbSet
                .Include(x => x.Company)
                .Include(x => x.User)
                .Where(x => x.Id == id)
                .AsNoTrackingWithIdentityResolution()
                .FirstOrDefaultAsync();
        }
    }
}
