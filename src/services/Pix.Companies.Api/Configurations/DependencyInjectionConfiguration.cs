using Pix.Microservices.Core.Jwt;
using Pix.Microservices.Domain.Repositories;
using Pix.Microservices.Infrastructure.Repositories;
using Pix.Microservices.Infrastructure.Transactions;
using Pix.Companies.Api.Service;

namespace Pix.Companies.Api.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IUow, Uow>();
            services.AddScoped<IJwtService, JwtService>();

            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<CompanyService>();

            services.AddScoped<ICompanyUserRepository, CompanyUserRepository>();
            services.AddScoped<CompanyUserService>();

            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
