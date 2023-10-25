using Pix.Microservices.Domain.Entities;
using Pix.Microservices.Domain.Model;
using Pix.Microservices.Domain.Repositories.Base;

namespace Pix.Microservices.Domain.Repositories
{

    public interface IUserRepository: IGenericRepository<User>
    {
        Task<PagedResponse<User, PagedResult>> GetAllAsync(PaginationFilter paginationFilter);
        Task<User> LoginAsync(string email, string password);
    }
}
