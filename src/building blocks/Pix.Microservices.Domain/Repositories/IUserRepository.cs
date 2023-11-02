using Pix.Microservices.Domain.Entities;
using Esterdigi.Api.Core.Database.Domain.Model;
using Esterdigi.Api.Core.Database.Domain.Repository.Base;

namespace Pix.Microservices.Domain.Repositories
{

    public interface IUserRepository: IGenericRepository<User>
    {
        Task<PagedResponse<User, PagedResult>> GetAllAsync(PaginationFilter paginationFilter);
        Task<User> LoginAsync(string email, string password);
    }
}
