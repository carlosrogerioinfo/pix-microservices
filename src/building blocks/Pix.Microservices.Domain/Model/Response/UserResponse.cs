using Pix.Microservices.Domain.Enums;
using Esterdigi.Api.Core.Commands;

namespace Pix.Microservices.Domain.Http.Response
{
    public class UserResponse : ICommandResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public UserType UserTypeId { get; set; }
        public string UserType { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
    }
}