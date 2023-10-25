using Pix.Microservices.Domain.Profiles;

namespace Pix.Banks.Api.Configurations
{
    public static class AutoMapperConfiguration
    {
        public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(BankProfile),
                typeof(BankAccountProfile),
                typeof(CompanyProfile),
                typeof(BankTransactionProfile),
                typeof(BankTransactionHistoryProfile),
                typeof(DeviceProfile),
                typeof(DeviceUserProfile),
                typeof(CompanyUserProfile)
            );

            return services;
        }
    }
}
