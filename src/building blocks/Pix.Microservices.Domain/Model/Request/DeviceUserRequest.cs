using Esterdigi.Api.Core.Commands;

namespace Pix.Microservices.Domain.Http.Request
{
    public class DeviceUserRegisterRequest :  ICommand
    {
        public bool Active { get; set; }
        public Guid DeviceId { get; set; }
        public Guid UserId { get; set; }
    }

    public class DeviceUserUpdateRequest : DeviceUserRegisterRequest, ICommand
    {
        public Guid Id { get; set; }
    }
}
