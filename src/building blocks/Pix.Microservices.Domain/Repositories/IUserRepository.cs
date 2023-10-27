using Pix.Microservices.Domain.Entities;
using Core.Integration.Domain.Model;
using Core.Integration.Domain.Repository.Base;

namespace Pix.Microservices.Domain.Repositories
{

    public interface IUserRepository: IGenericRepository<User>
    {
        Task<PagedResponse<User, PagedResult>> GetAllAsync(PaginationFilter paginationFilter);
        Task<User> LoginAsync(string email, string password);
    }
}
