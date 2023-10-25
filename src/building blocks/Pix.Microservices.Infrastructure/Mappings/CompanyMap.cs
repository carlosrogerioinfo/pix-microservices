using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pix.Microservices.Domain.Entities;
using Pix.Core.Lib.Constants;

namespace Pix.Microservices.Infrastructure.Mappings
{
    public class CompanyMap : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> entity)
        {
            //Entity
            entity.ToTable("Companies");
            entity.HasKey(x => x.Id);

            //Properties
            entity.Property(x => x.CompanyName).IsRequired().HasMaxLength(150).HasColumnType(Constants.Varchar);
            entity.Property(x => x.TradingName).IsRequired().HasMaxLength(150).HasColumnType(Constants.Varchar);
            entity.Property(x => x.Cnpj).IsRequired().HasMaxLength(14).HasColumnType(Constants.Varchar);
            entity.Property(x => x.Email).IsRequired().HasMaxLength(100).HasColumnType(Constants.Varchar);
            entity.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(14).HasColumnType(Constants.Varchar);
            entity.Property(x => x.Contact).IsRequired().HasMaxLength(50).HasColumnType(Constants.Varchar);
            entity.Property(x => x.Active).IsRequired().HasColumnType(Constants.BooleanPostgreSql);
            entity.Property(x => x.CreatedAt).HasColumnType(Constants.DateTimePostgreSql);
            entity.Property(x => x.LastUpdatedAt).HasColumnType(Constants.DateTimePostgreSql);

            //Ignore equivalent NotMapping
            entity.Ignore(x => x.Notifications);

            //Relationchip cardinality
            entity
                .HasMany(a => a.BankAccounts)
                .WithOne(c => c.Company)
                .IsRequired()
                .HasForeignKey(c => c.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            entity
                .HasMany(a => a.BankTransactions)
                .WithOne(c => c.Company)
                .IsRequired()
                .HasForeignKey(c => c.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            entity
                .HasMany(a => a.Devices)
                .WithOne(c => c.Company)
                .IsRequired()
                .HasForeignKey(c => c.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

            entity
                .HasMany(a => a.CompanyUsers)
                .WithOne(c => c.Company)
                .IsRequired()
                .HasForeignKey(c => c.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}