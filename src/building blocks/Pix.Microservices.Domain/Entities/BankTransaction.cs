using FluentValidator;
using Pix.Microservices.Domain.Enums;
using Pix.Core.Lib.Entities;

namespace Pix.Microservices.Domain.Entities
{
    public class BankTransaction: Entity
    {
        protected BankTransaction() { }

        public BankTransaction(Guid id, double amount, string transactionId, DateTime? paymentDate, 
            string description, string authenticationCode, Guid bankId, Guid companyId, Guid userId, 
            StatusCodeType statusCodeType, string qrCode, Guid bankAccountId, Guid deviceId, 
            Guid? idempotentId, Guid? payerBankId, string payerName, string payerDescription,
            DateTime createdAt = default)
        {
            if (id != Guid.Empty) Id = id;
            CreatedAt = (id == Guid.Empty ? DateTime.Now : createdAt);
            LastUpdatedAt = (id == Guid.Empty ? null : DateTime.Now);

            Amount = amount;
            TransactionId = transactionId;
            IdempotentId = idempotentId;
            PaymentDate = (paymentDate.HasValue ? paymentDate.Value : null);
            Description = description;
            AuthenticationCode = authenticationCode;
            BankId = bankId;
            CompanyId = companyId;
            BankAccountId = bankAccountId;
            PayerBankId = payerBankId;
            UserId = userId;
            DeviceId = deviceId;
            StatusCodeType = statusCodeType;
            QrCode = qrCode;
            PayerName = payerName;
            PayerDescription = payerDescription;

            new ValidationContract<BankTransaction>(this)

                ;

        }

        public double Amount { get; private set; }
        public string TransactionId { get; private set; }
        public Guid? IdempotentId { get; private set; }
        public DateTime? PaymentDate { get; private set; }
        public string Description { get; private set; }
        public string AuthenticationCode { get; private set; }
        public string QrCode { get; private set; }
        public StatusCodeType StatusCodeType { get; private set; }
        public string PayerName { get; private set; }
        public Guid? PayerBankId { get; private set; }
        public string PayerDescription { get; private set; }

        public Guid BankId { get; set; }
        public Bank Bank { get; set; }

        public Guid BankAccountId { get; set; }
        public BankAccount BankAccount { get; set; }

        public Guid CompanyId { get; set; }
        public Company Company { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid DeviceId { get; set; }
        public Device Device { get; set; }

        public IEnumerable<BankTransactionHistory> BankTransactionHistories { get; } = new List<BankTransactionHistory>();
    }
}
