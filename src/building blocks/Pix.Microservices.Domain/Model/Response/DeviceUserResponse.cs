using Pix.Microservices.Domain.Entities;
using Esterdigi.Api.Core.Commands;

namespace Pix.Microservices.Domain.Http.Response
{
    public class DeviceUserResponse : ICommandResult
    {
        public Guid Id { get; set; }
        public bool Active { get; set; }
        public DeviceResponse Device { get; set; }
        public UserResponse User { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastUpdatedAt { get; set; }

    }
}