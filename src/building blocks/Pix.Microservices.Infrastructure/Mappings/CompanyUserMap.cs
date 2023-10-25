using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pix.Microservices.Domain.Entities;
using Pix.Core.Lib.Constants;

namespace Pix.Microservices.Infrastructure.Mappings
{
    public class CompanyUserMap : IEntityTypeConfiguration<CompanyUser>
    {
        public void Configure(EntityTypeBuilder<CompanyUser> entity)
        {
            //Entity
            entity.ToTable("CompanyUsers");
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