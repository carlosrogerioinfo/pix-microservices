using Esterdigi.Api.Core.Commands;

namespace Pix.Microservices.Domain.Http.Response
{
    public class BankResponse : ICommandResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
    }
}
