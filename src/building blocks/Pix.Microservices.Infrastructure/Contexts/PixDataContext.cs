using Microsoft.EntityFrameworkCore;
using Pix.Microservices.Domain.Entities;
using Pix.Microservices.Infrastructure.Mappings;

namespace Pix.Microservices.Infrastructure.Contexts
{
    public class PixDataContext : DbContext
    {
        public PixDataContext() { }

        public PixDataContext(DbContextOptions<PixDataContext> options) : base(options) { }

        public DbSet<Bank> Banks { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<BankTransaction> BankTransactions { get; set; }
        public DbSet<BankTransactionHistory> BankTransactionHistories { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceUser> DeviceUsers { get; set; }
        public DbSet<CompanyUser> CompanyUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            /* Descomentar abaixo quando precisar gerar a migration ou realizar alguma alteração no banco de dados */

            //SQL Server 
            //options.UseSqlServer("Server=url_server;Database=database_name;User ID=username;Password=password;");

            //Postgre SQL
            //options.UseNpgsql("Host=url_server;Port=port;Pooling=true;Database=database_name;User Id=username;Password=password;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BankMap());
            modelBuilder.ApplyConfiguration(new BankAccountMap());
            modelBuilder.ApplyConfiguration(new CompanyMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new BankTransactionMap());
            modelBuilder.ApplyConfiguration(new BankTransactionHistoryMap());
            modelBuilder.ApplyConfiguration(new DeviceMap());
            modelBuilder.ApplyConfiguration(new DeviceUserMap());
            modelBuilder.ApplyConfiguration(new CompanyUserMap());

        }
    }
}