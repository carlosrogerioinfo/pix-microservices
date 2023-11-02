using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pix.Microservices.Domain.Entities;
using Esterdigi.Api.Core.Constants;

namespace Pix.Microservices.Infrastructure.Mappings
{
    public class BankAccountMap : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> entity)
        {
            //Entity
            entity.ToTable("BankAccounts");
            entity.HasKey(x => x.Id);

            //Properties
            entity.Property(x => x.AccountNumber).IsRequired().HasMaxLength(30).HasColumnType(Constants.Varchar);
            entity.Property(x => x.AccountDigit).IsRequired().HasMaxLength(2).HasColumnType(Constants.Varchar);
            entity.Property(x => x.Agency).IsRequired().HasMaxLength(20).HasColumnType(Constants.Varchar);
            entity.Property(x => x.Active).IsRequired().HasColumnType(Constants.BooleanPostgreSql);
            entity.Property(x => x.AccountType).HasColumnType(Constants.Integer);
            entity.Property(x => x.CreatedAt).HasColumnType(Constants.DateTimePostgreSql);
            entity.Property(x => x.LastUpdatedAt).HasColumnType(Constants.DateTimePostgreSql);

            //Ignore equivalent NotMapping
            entity.Ignore(x => x.Notifications);

            //Relationchip cardinality

            entity
                .HasMany(a => a.BankTransactions)
                .WithOne(c => c.BankAccount)
                .IsRequired()
                .HasForeignKey(c => c.BankAccountId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}