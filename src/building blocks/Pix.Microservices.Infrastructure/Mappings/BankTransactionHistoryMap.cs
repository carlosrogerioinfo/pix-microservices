using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Esterdigi.Api.Core.Constants;
using Pix.Microservices.Domain.Entities;

namespace Pix.Microservices.Infrastructure.Mappings
{
    public class BankTransactionHistoryMap : IEntityTypeConfiguration<BankTransactionHistory>
    {
        public void Configure(EntityTypeBuilder<BankTransactionHistory> entity)
        {
            //Entity
            entity.ToTable("BankTransactionHistories");
            entity.HasKey(x => x.Id);

            //Properties
            entity.Property(x => x.Status).HasColumnType(Constants.Varchar);
            entity.Property(x => x.Request).HasColumnType(Constants.Varchar);
            entity.Property(x => x.Response).HasColumnType(Constants.Varchar);
            entity.Property(x => x.CreatedAt).HasColumnType(Constants.DateTimePostgreSql);

            //Ignore equivalent NotMapping
            entity.Ignore(x => x.Notifications);

            //Relationchip cardinality
        }
    }
}