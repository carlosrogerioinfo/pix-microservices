using Esterdigi.Api.Core.Commands;

namespace Pix.Microservices.Domain.Http.Response
{
    public class CompanyUserResponse : ICommandResult
    {
        public Guid Id { get; set; }
        public bool Active { get; set; }
        public CompanyResponse Company { get; set; }
        public UserResponse User { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastUpdatedAt { get; set; }

    }
}