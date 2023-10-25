using Microsoft.EntityFrameworkCore;
using Pix.Microservices.Domain.Entities;
using Core.Repository.Domain.Extention;
using Core.Repository.Domain.Model;
using Pix.Microservices.Domain.Repositories;
using Pix.Microservices.Infrastructure.Contexts;
using Core.Repository.Infrastructure.Repository.Base;
using Core.Repository.Domain.Extention;

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
