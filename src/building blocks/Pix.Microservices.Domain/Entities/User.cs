using FluentValidator;
using Pix.Microservices.Domain.Enums;
using Pix.Core.Lib.Entities;

namespace Pix.Microservices.Domain.Entities
{
    public class User: Entity
    {
        protected User() { }

        public User(Guid id, string name, string email, UserType userType, DateTime createdAt = default)
        {
            if (id != Guid.Empty) Id = id;
            CreatedAt = (id == Guid.Empty ? DateTime.Now : createdAt);
            LastUpdatedAt = (id == Guid.Empty ? null : DateTime.Now);

            Name = name;
            Email = email;
            UserType = userType;

            new ValidationContract<User>(this)
                .IsRequired(x => x.Name, "O nome deve ser informado")
                .IsRequired(x => x.Email, "O e-mail deve ser informado")
                .IsEmail(x => x.Email, "O e-mail informado deve ser um e-mail válido")
                ;
        }

        public string Name { get; private set; }
        public string Email { get; private set; }
        public UserType UserType { get; private set; }

        public IEnumerable<BankTransaction> BankTransactions { get; } = new List<BankTransaction>();
        public IEnumerable<DeviceUser> DeviceUsers { get; } = new List<DeviceUser>();
        public IEnumerable<CompanyUser> CompanyUsers { get; } = new List<CompanyUser>();
    }
}
