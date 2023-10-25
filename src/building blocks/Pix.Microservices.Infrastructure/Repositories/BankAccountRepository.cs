using Core.Repository.Infrastructure.Repository.Base;
using Pix.Microservices.Domain.Entities;
using Pix.Microservices.Domain.Repositories;
using Pix.Microservices.Infrastructure.Contexts;

namespace Pix.Microservices.Infrastructure.Repositories
{
    public class BankAccountRepository : GenericRepository<BankAccount>, IBankAccountRepository
    {
        public BankAccountRepository(PixDataContext context) : base(context)
        {

        }


    }
}
