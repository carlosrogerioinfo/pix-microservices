using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pix.Microservices.Domain.Entities;
using Pix.Core.Lib.Constants;

namespace Pix.Microservices.Infrastructure.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            //Entity
            entity.ToTable("Users");
            entity.HasKey(x => x.Id);

            //Properties
            entity.Property(x => x.Name).IsRequired().HasMaxLength(150).HasColumnType(Constants.Varchar);
            entity.Property(x => x.Email).IsRequired().HasMaxLength(100).HasColumnType(Constants.Varchar);
            entity.Property(x => x.UserType).HasColumnType(Constants.Integer);
            entity.Property(x => x.CreatedAt).HasColumnType(Constants.DateTimePostgreSql);
            entity.Property(x => x.LastUpdatedAt).HasColumnType(Constants.DateTimePostgreSql);

            //Ignore equivalent NotMapping
            entity.Ignore(x => x.Notifications);

            //Relationchip cardinality

            entity
                .HasMany(a => a.BankTransactions)
                .WithOne(c => c.User)
                .IsRequired()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity
                .HasMany(a => a.DeviceUsers)
                .WithOne(c => c.User)
                .IsRequired()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity
                .HasMany(a => a.CompanyUsers)
                .WithOne(c => c.User)
                .IsRequired()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}