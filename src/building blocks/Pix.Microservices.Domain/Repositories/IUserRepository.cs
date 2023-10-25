using Pix.Microservices.Domain.Entities;
using Core.Repository.Domain.Model;
using Core.Repository.Domain.Repository.Base;

namespace Pix.Microservices.Domain.Repositories
{

    public interface IUserRepository: IGenericRepository<User>
    {
        Task<PagedResponse<User, PagedResult>> GetAllAsync(PaginationFilter paginationFilter);
        Task<User> LoginAsync(string email, string password);
    }
}
