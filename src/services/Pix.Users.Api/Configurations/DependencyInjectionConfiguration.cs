using Pix.Microservices.Core.Jwt;
using Pix.Microservices.Domain.Repositories;
using Pix.Microservices.Infrastructure.Repositories;
using Pix.Microservices.Infrastructure.Transactions;
using Pix.Users.Api.Service;

namespace Pix.Users.Api.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IUow, Uow>();
            services.AddScoped<IJwtService, JwtService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<UserService>();

            return services;
        }
    }
}
