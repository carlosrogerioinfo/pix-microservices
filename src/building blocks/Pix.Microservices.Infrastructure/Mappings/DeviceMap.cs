using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pix.Microservices.Domain.Entities;
using Pix.Core.Lib.Constants;

namespace Pix.Microservices.Infrastructure.Mappings
{
    public class DeviceMap : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> entity)
        {
            //Entity
            entity.ToTable("Devices");
            entity.HasKey(x => x.Id);

            //Properties
            entity.Property(x => x.Name).IsRequired().HasMaxLength(150).HasColumnType(Constants.Varchar);
            entity.Property(x => x.Platform).IsRequired().HasMaxLength(100).HasColumnType(Constants.Varchar);
            entity.Property(x => x.PlatformVersion).IsRequired().HasMaxLength(50).HasColumnType(Constants.Varchar);
            entity.Property(x => x.Model).IsRequired().HasMaxLength(50).HasColumnType(Constants.Varchar);
            entity.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(14).HasColumnType(Constants.Varchar);
            entity.Property(x => x.CreatedAt).HasColumnType(Constants.DateTimePostgreSql);
            entity.Property(x => x.LastUpdatedAt).HasColumnType(Constants.DateTimePostgreSql);

            //Ignore equivalent NotMapping
            entity.Ignore(x => x.Notifications);

            //Relationchip cardinality

            entity
                .HasMany(a => a.BankTransactions)
                .WithOne(c => c.Device)
                .IsRequired()
                .HasForeignKey(c => c.DeviceId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}