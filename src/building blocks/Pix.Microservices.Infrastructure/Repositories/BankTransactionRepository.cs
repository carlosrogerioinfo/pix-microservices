using Pix.Microservices.Domain.Entities;
using Pix.Microservices.Domain.Repositories;
using Pix.Microservices.Infrastructure.Contexts;
using Core.Integration.Infrastructure.Repository.Base;

namespace Pix.Microservices.Infrastructure.Repositories
{
    public class BankTransactionRepository : GenericRepository<BankTransaction>, IBankTransactionRepository
    {
        public BankTransactionRepository(PixDataContext context) : base(context)
        {

        }

    }
}
