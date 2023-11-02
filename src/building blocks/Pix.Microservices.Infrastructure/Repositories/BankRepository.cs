using Pix.Microservices.Domain.Entities;
using Pix.Microservices.Domain.Repositories;
using Pix.Microservices.Infrastructure.Contexts;
using Esterdigi.Api.Core.Database.Infrastructure.Repository.Base;

namespace Pix.Microservices.Infrastructure.Repositories
{
    public class BankRepository : GenericRepository<Bank>, IBankRepository
    {
        public BankRepository(PixDataContext context) : base(context)
        {
        }

    }
}
