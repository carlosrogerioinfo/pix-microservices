using FluentValidator;
using Esterdigi.Api.Core.Database.Domain.Entities;

namespace Pix.Microservices.Domain.Entities
{
    public class Device: Entity
    {
        protected Device() { }

        public Device(Guid id, string name, string platform, string platformVersion, string model, string phoneNumber, Guid companyId, DateTime createdAt = default)
        {
            if (id != Guid.Empty) Id = id;
            CreatedAt = (id == Guid.Empty ? DateTime.Now : createdAt);
            LastUpdatedAt = (id == Guid.Empty ? null : DateTime.Now);

            Name = name;
            Platform = platform;
            PlatformVersion = platformVersion;
            Model = model;
            PhoneNumber = phoneNumber;
            CompanyId = companyId;

            new ValidationContract<Device>(this)
                .IsRequired(x => x.Name, "O nome do dispositivo deve ser informado")
                .IsRequired(x => x.Platform, "A plataforma do dispositivo deve ser informado")
                .IsRequired(x => x.PlatformVersion, "Versão da plataforma do dispositivo deve ser informado")
                .IsRequired(x => x.Model, "O modelo do dispositivo deve ser informado")
                .IsRequired(x => x.PhoneNumber, "O número do dispositivo deve ser informado")
                ;

        }

        public string Name { get; private set; }
        public string Platform { get; private set; }
        public string PlatformVersion { get; private set; }
        public string Model { get; private set; }
        public string PhoneNumber { get; private set; }

        public Guid CompanyId { get; set; }
        public Company Company { get; set; }

        public IEnumerable<BankTransaction> BankTransactions { get; } = new List<BankTransaction>();

        public IEnumerable<DeviceUser> DeviceUsers { get; } = new List<DeviceUser>();

    }
}
