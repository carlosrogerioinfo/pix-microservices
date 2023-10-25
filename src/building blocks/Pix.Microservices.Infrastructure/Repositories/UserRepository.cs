using Microsoft.EntityFrameworkCore;
using Pix.Microservices.Domain.Entities;
using Pix.Microservices.Domain.Extention;
using Pix.Microservices.Domain.Model;
using Pix.Microservices.Domain.Repositories;
using Pix.Microservices.Infrastructure.Contexts;
using Pix.Microservices.Infrastructure.Repositories.Base;

namespace Pix.Microservices.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(PixDataContext context) : base(context)
        {

        }

        public async Task<PagedResponse<User, PagedResult>> GetAllAsync(PaginationFilter paginationFilter)
        {
            var queryable = await _dbSet.GetPagedAsync(paginationFilter.Page, paginationFilter.PageSize);

            var entity = await _dbSet
                .AsNoTrackingWithIdentityResolution()
                .Skip((paginationFilter.Page - 1) * paginationFilter.PageSize)
                .Take(paginationFilter.PageSize)
                .ToListAsync();

            return new PagedResponse<User, PagedResult>(entity, queryable);
        }

        public async Task<User> LoginAsync(string email, string password)
        {
            return await _dbSet
                .AsNoTrackingWithIdentityResolution()
                .FirstOrDefaultAsync(x => x.Email == email);
        }

    }
}
