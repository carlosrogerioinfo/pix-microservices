using Pix.Microservices.Domain.Entities;
using Pix.Microservices.Domain.Model;
using Pix.Microservices.Domain.Repositories.Base;

namespace Pix.Microservices.Domain.Repositories
{

    public interface ICompanyUserRepository: IGenericRepository<CompanyUser>
    {
        Task<PagedResponse<CompanyUser, PagedResult>> GetAllAsync(PaginationFilter paginationFilter);
        Task<CompanyUser> GetAsync(Guid id);
    }
}
