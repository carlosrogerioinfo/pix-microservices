using Pix.Microservices.Domain.Enums;
using Pix.Core.Lib.Commands;

namespace Pix.Microservices.Domain.Http.Response
{
    public class BankAccountResponse : ICommandResult
    {
        public Guid Id { get; set; }
        public string AccountNumber { get; set; }
        public string AccountDigit { get; set; }
        public string Agency { get; set; }
        public AccountType AccountTypeId { get; set; }
        public string AccountType { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastUpdatedAt { get; set; }

        public BankResponse Bank { get; set; }
        public CompanyResponse Company { get; set; }
    }
}