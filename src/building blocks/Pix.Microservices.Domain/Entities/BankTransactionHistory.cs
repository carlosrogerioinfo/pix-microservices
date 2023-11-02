using FluentValidator;
using Esterdigi.Api.Core.Database.Domain.Entities;

namespace Pix.Microservices.Domain.Entities
{
    public class BankTransactionHistory: Entity
    {
        protected BankTransactionHistory() { }

        public BankTransactionHistory(Guid id, Guid bankTransactionId, string status, string request, string response, DateTime createdAt = default)
        {
            if (id != Guid.Empty) Id = id;
            CreatedAt = (id == Guid.Empty ? DateTime.Now : createdAt);
            LastUpdatedAt = (id == Guid.Empty ? null : DateTime.Now);

            BankTransactionId = bankTransactionId;
            Status = status;
            Request = request;
            Response = response;

            new ValidationContract<BankTransactionHistory>(this)

                ;

        }

        public string Status { get; private set; }
        public string Request { get; private set; }
        public string Response { get; private set; }

        public Guid BankTransactionId { get; set; }
        public BankTransaction BankTransaction { get; set; }
    }
}
