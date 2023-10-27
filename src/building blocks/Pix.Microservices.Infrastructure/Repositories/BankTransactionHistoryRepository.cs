using Pix.Microservices.Domain.Entities;
using Pix.Microservices.Domain.Repositories;
using Pix.Microservices.Infrastructure.Contexts;
using Core.Integration.Infrastructure.Repository.Base;

namespace Pix.Microservices.Infrastructure.Repositories
{
    public class BankTransactionHistoryRepository : GenericRepository<BankTransactionHistory>, IBankTransactionHistoryRepository
    {
        public BankTransactionHistoryRepository(PixDataContext context) : base(context)
        {

        }
        
    }
}
