using Core.Repository.Domain.Model;
using Core.Repository.Domain.Repository.Base;
using Pix.Microservices.Domain.Entities;

namespace Pix.Microservices.Domain.Repositories
{

    public interface ICompanyUserRepository: IGenericRepository<CompanyUser>
    {
        Task<PagedResponse<CompanyUser, PagedResult>> GetAllAsync(PaginationFilter paginationFilter);
        Task<CompanyUser> GetAsync(Guid id);
    }
}
