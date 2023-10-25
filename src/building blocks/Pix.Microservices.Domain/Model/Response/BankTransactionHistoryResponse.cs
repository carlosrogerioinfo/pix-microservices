using Pix.Core.Lib.Commands;

namespace Pix.Microservices.Domain.Http.Response
{
    public class BankTransactionHistoryResponse : ICommandResult
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastUpdatedAt { get; set; }

        public BankTransactionResponse BankTransaction { get; set; }
    }
}