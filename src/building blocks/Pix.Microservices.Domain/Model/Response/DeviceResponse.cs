using Esterdigi.Api.Core.Commands;

namespace Pix.Microservices.Domain.Http.Response
{
    public class DeviceResponse : ICommandResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Platform { get; set; }
        public string PlatformVersion { get; set; }
        public string Model { get; set; }
        public string PhoneNumber { get; set; }
        public CompanyResponse Company { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastUpdatedAt { get; set; }

    }
}