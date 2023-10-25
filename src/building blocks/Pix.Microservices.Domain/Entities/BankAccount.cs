using FluentValidator;
using Pix.Microservices.Domain.Enums;
using Pix.Core.Lib.Entities;

namespace Pix.Microservices.Domain.Entities
{
    public class BankAccount: Entity
    {
        protected BankAccount() { }

        public BankAccount(Guid id, string accountNumber, string accountDigit, AccountType accountType, Guid bankId, Guid companyId, string agency, bool active, DateTime createdAt = default)
        {
            if (id != Guid.Empty) Id = id;
            CreatedAt = (id == Guid.Empty ? DateTime.Now : createdAt);
            LastUpdatedAt = (id == Guid.Empty ? null : DateTime.Now);

            BankId = bankId;
            Agency = agency;
            AccountNumber = accountNumber;
            AccountDigit = accountDigit;
            AccountType = accountType;
            Active = active;
            CompanyId = companyId;

            new ValidationContract<BankAccount>(this)
                .IsRequired(x => x.AccountNumber, "O número da conta bancária deve ser informada informado")
                ;
        }

        public string AccountNumber { get; private set; }
        public string AccountDigit { get; set; }
        public string Agency { get; set; }
        public AccountType AccountType { get; set; }
        public bool Active { get; private set; }

        public Guid BankId { get; set; }
        public Bank Bank { get; set; }
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }

        public IEnumerable<BankTransaction> BankTransactions { get; } = new List<BankTransaction>();

        public void Activate() => Active = true;
        public void Deactivate() => Active = false;

    }
}
