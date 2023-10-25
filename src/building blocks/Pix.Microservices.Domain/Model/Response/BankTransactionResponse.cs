using Pix.Microservices.Domain.Enums;
using Pix.Core.Lib.Commands;

namespace Pix.Microservices.Domain.Http.Response
{
    public class BankTransactionResponse : ICommandResult
    {
        public Guid Id { get; set; }
        public double Amount { get; set; }
        public string TransactionId { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Description { get; set; }
        public string AuthenticationCode { get; set; }
        public bool PaymentConfirmed { get; set; }
        public string QrCode { get; private set; }
        public StatusCodeType StatusCodeTypeId { get; set; }
        public string StatusCodeType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastUpdatedAt { get; set; }

        public BankResponse Bank { get; set; }
        public BankAccountResponse BankAccount { get; set; }
        public CompanyResponse Company { get; set; }
        public UserResponse User { get; set; }
        public DeviceResponse Device { get; set; }
    }
}