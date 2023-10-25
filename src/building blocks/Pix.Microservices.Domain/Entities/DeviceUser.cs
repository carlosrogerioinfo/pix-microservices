using FluentValidator;
using Pix.Core.Lib.Entities;

namespace Pix.Microservices.Domain.Entities
{
    public class DeviceUser: Entity
    {
        protected DeviceUser() { }

        public DeviceUser(Guid id, bool active, Guid deviceId, Guid userId, DateTime createdAt = default)
        {
            if (id != Guid.Empty) Id = id;
            CreatedAt = (id == Guid.Empty ? DateTime.Now : createdAt);
            LastUpdatedAt = (id == Guid.Empty ? null : DateTime.Now);

            Active = active;
            DeviceId = deviceId;
            UserId = userId;

            new ValidationContract<DeviceUser>(this)
                ;

        }

        public bool Active { get; private set; }

        public Guid DeviceId { get; set; }
        public Device Device { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
