using Pix.Microservices.Domain.Entities;
using Pix.Core.Lib.Commands;

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