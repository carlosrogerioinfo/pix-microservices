using Pix.Microservices.Infrastructure.Contexts;

namespace Pix.Microservices.Infrastructure.Transactions
{
    public class Uow : IUow
    {
        private readonly PixDataContext _context;

        public Uow(PixDataContext context)
        {
            _context = context;
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Rollback()
        {
            // Do Nothing
        }
    }
}
