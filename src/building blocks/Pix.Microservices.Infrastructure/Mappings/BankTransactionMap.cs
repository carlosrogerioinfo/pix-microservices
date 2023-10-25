using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pix.Microservices.Domain.Entities;
using Pix.Core.Lib.Constants;

namespace Pix.Microservices.Infrastructure.Mappings
{
    public class BankTransactionMap : IEntityTypeConfiguration<BankTransaction>
    {
        public void Configure(EntityTypeBuilder<BankTransaction> entity)
        {
            //Entity
            entity.ToTable("BankTransactions");
            entity.HasKey(x => x.Id);

            //Properties
            entity.Property(x => x.Amount).IsRequired().HasColumnType(Constants.Double);
            entity.Property(x => x.TransactionId).IsRequired().HasColumnType(Constants.Varchar);
            entity.Property(x => x.IdempotentId).HasColumnType(Constants.GuidPostgreSql);
            entity.Property(x => x.PaymentDate).HasColumnType(Constants.DateTimePostgreSql);
            entity.Property(x => x.Description).HasMaxLength(250).HasColumnType(Constants.Varchar);
            entity.Property(x => x.AuthenticationCode).HasMaxLength(250).HasColumnType(Constants.Varchar);
            entity.Property(x => x.PayerName).HasMaxLength(150).HasColumnType(Constants.Varchar);
            entity.Property(x => x.PayerDescription).HasMaxLength(150).HasColumnType(Constants.Varchar);
            entity.Property(x => x.PayerBankId).HasColumnType(Constants.GuidPostgreSql);
            entity.Property(x => x.QrCode).HasColumnType(Constants.Text);
            entity.Property(x => x.StatusCodeType).HasColumnType(Constants.Integer);
            entity.Property(x => x.CreatedAt).HasColumnType(Constants.DateTimePostgreSql);
            entity.Property(x => x.LastUpdatedAt).HasColumnType(Constants.DateTimePostgreSql);

            //Ignore equivalent NotMapping
            entity.Ignore(x => x.Notifications);

            //Relationchip cardinality

            entity
                .HasMany(a => a.BankTransactionHistories)
                .WithOne(c => c.BankTransaction)
                .IsRequired()
                .HasForeignKey(c => c.BankTransactionId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}