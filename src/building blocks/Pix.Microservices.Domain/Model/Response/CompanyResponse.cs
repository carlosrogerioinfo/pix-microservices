using Pix.Core.Lib.Commands;

namespace Pix.Microservices.Domain.Http.Response
{
    public class CompanyResponse : ICommandResult
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public string TradingName { get; set; }
        public string Cnpj { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Contact { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastUpdatedAt { get; set; }

    }
}