using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pix.Microservices.Domain.Entities;
using Pix.Core.Lib.Constants;

namespace Pix.Microservices.Infrastructure.Mappings
{
    public class BankMap : IEntityTypeConfiguration<Bank>
    {
        public void Configure(EntityTypeBuilder<Bank> entity)
        {
            //Entity
            entity.ToTable("Banks");
            entity.HasKey(x => x.Id);

            //Properties
            entity.Property(x => x.Name).IsRequired().HasMaxLength(100).HasColumnType(Constants.Varchar);
            entity.Property(x => x.Number).IsRequired().HasColumnType(Constants.SmallInt);
            entity.Property(x => x.Active).IsRequired().HasColumnType(Constants.BooleanPostgreSql);
            entity.Property(x => x.CreatedAt).HasColumnType(Constants.DateTimePostgreSql);
            entity.Property(x => x.LastUpdatedAt).HasColumnType(Constants.DateTimePostgreSql);

            //Ignore equivalent NotMapping
            entity.Ignore(x => x.Notifications);

            //Relationchip cardinality
            entity
                .HasMany(a => a.BankAccounts)
                .WithOne(c => c.Bank)
                .IsRequired()
                .HasForeignKey(c => c.BankId)
                .OnDelete(DeleteBehavior.Cascade);

            entity
                .HasMany(a => a.BankTransactions)
                .WithOne(c => c.Bank)
                .IsRequired()
                .HasForeignKey(c => c.BankId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}