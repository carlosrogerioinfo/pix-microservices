using Pix.Microservices.Core.Jwt;
using Pix.Microservices.Domain.Repositories;
using Pix.Microservices.Infrastructure.Repositories;
using Pix.Microservices.Infrastructure.Transactions;
using Pix.Banks.Api.Service;

namespace Pix.Banks.Api.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IUow, Uow>();
            services.AddScoped<IJwtService, JwtService>();

            services.AddScoped<IBankRepository, BankRepository>();
            services.AddScoped<BankService>();

            services.AddScoped<IBankAccountRepository, BankAccountRepository>();
            services.AddScoped<BankAccountService>();

            services.AddScoped<IBankTransactionRepository, BankTransactionRepository>();
            services.AddScoped<BankTransactionService>();

            services.AddScoped<IBankTransactionHistoryRepository, BankTransactionHistoryRepository>();
            services.AddScoped<BankTransactionHistoryService>();

            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
