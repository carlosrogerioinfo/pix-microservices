using FluentValidator;
using Esterdigi.Api.Core.Database.Domain.Entities;

namespace Pix.Microservices.Domain.Entities
{
    public class CompanyUser: Entity
    {
        protected CompanyUser() { }

        public CompanyUser(Guid id, bool active, Guid companyId, Guid userId, DateTime createdAt = default)
        {
            if (id != Guid.Empty) Id = id;
            CreatedAt = (id == Guid.Empty ? DateTime.Now : createdAt);
            LastUpdatedAt = (id == Guid.Empty ? null : DateTime.Now);

            Active = active;
            CompanyId = companyId;
            UserId = userId;

            new ValidationContract<CompanyUser>(this)
                ;

        }

        public bool Active { get; private set; }

        public Guid CompanyId { get; set; }
        public Company Company { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
