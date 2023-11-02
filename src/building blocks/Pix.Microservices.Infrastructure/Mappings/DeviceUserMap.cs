using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pix.Microservices.Domain.Entities;
using Esterdigi.Api.Core.Constants;

namespace Pix.Microservices.Infrastructure.Mappings
{
    public class DeviceUserMap : IEntityTypeConfiguration<DeviceUser>
    {
        public void Configure(EntityTypeBuilder<DeviceUser> entity)
        {
            //Entity
            entity.ToTable("DeviceUsers");
            //entity.HasKey(x => x.Id);

            //Properties
            entity.Property(x => x.Active).IsRequired().HasColumnType(Constants.BooleanPostgreSql);
            entity.Property(x => x.CreatedAt).HasColumnType(Constants.DateTimePostgreSql);
            entity.Property(x => x.LastUpdatedAt).HasColumnType(Constants.DateTimePostgreSql);

            //Ignore equivalent NotMapping
            entity.Ignore(x => x.Notifications);

            //Relationchip cardinality


        }
    }
}