using Esterdigi.Api.Core.Database.Domain.Entities;
using FluentValidator;

namespace Pix.Microservices.Domain.Entities
{
    public class Bank: Entity
    {
        protected Bank() { }

        public Bank(Guid id, string name, int number, bool active, DateTime createdAt = default)
        {
            if (id != Guid.Empty) Id = id;
            CreatedAt = (id == Guid.Empty ? DateTime.Now : createdAt);
            LastUpdatedAt = (id == Guid.Empty ? null : DateTime.Now);

            Name = name;
            Number = number;
            Active = active;

            new ValidationContract<Bank>(this)
                .IsRequired(x => x.Name, "O nome do banco deve ser informado")
                ;
        }

        public string Name { get; private set; }
        public int Number { get; private set; }
        public bool Active { get; private set; }

        public IEnumerable<BankAccount> BankAccounts { get; } = new List<BankAccount>();
        public IEnumerable<BankTransaction> BankTransactions { get; } = new List<BankTransaction>();

        public void Activate() => Active = true;
        public void Deactivate() => Active = false;

    }
}