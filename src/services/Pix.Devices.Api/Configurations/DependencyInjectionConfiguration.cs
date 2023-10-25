using Pix.Microservices.Core.Jwt;
using Pix.Microservices.Domain.Repositories;
using Pix.Microservices.Infrastructure.Repositories;
using Pix.Microservices.Infrastructure.Transactions;
using Pix.Devices.Api.Service;

namespace Pix.Devices.Api.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IUow, Uow>();
            services.AddScoped<IJwtService, JwtService>();

            services.AddScoped<IDeviceRepository, DeviceRepository>();
            services.AddScoped<DeviceService>();

            services.AddScoped<IDeviceUserRepository, DeviceUserRepository>();
            services.AddScoped<DeviceUserService>();

            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
