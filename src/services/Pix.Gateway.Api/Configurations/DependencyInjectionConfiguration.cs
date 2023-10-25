using Pix.Gateway.Api.Service;
using Polly;
using Pix.Core.Lib.Extensions;
using System.Net.Http.Headers;

namespace Pix.Gateway.Api.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection AddDependencyInjectionConfiguration(this IServiceCollection services, IConfiguration configuration = default)
        {

            var baseUrl = (configuration.GetSection("UseLocal").Value.ToLower().Trim() == "true" ? "BaseUrl-Local" : "BaseUrl");
            var tryAlowedBeforeBreak = Convert.ToInt16(configuration.GetSection("ResiliencyConfigurations")["TryAlowedBeforeBreak"]);
            var durationOfBreak = Convert.ToDouble(configuration.GetSection("ResiliencyConfigurations")["DurationOfBreak"]);

            services.AddHttpClient<BankService>(config =>
            {
                config.BaseAddress = new Uri(configuration.GetSection(baseUrl)["PixSevenBackofficeApi"]);
                config.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            })
            .AddPolicyHandler(PollyExtensions.WaitAndRetry())
            .AddTransientHttpErrorPolicy(x => x.CircuitBreakerAsync(tryAlowedBeforeBreak, TimeSpan.FromSeconds(durationOfBreak)));

            services.AddHttpClient<BankAccountService>(config =>
            {
                config.BaseAddress = new Uri(configuration.GetSection(baseUrl)["PixSevenBackofficeApi"]);
                config.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            })
            .AddPolicyHandler(PollyExtensions.WaitAndRetry())
            .AddTransientHttpErrorPolicy(x => x.CircuitBreakerAsync(tryAlowedBeforeBreak, TimeSpan.FromSeconds(durationOfBreak)));

            services.AddHttpClient<CompanyService>(config =>
            {
                config.BaseAddress = new Uri(configuration.GetSection(baseUrl)["PixSevenBackofficeApi"]);
                config.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            })
            .AddPolicyHandler(PollyExtensions.WaitAndRetry())
            .AddTransientHttpErrorPolicy(x => x.CircuitBreakerAsync(tryAlowedBeforeBreak, TimeSpan.FromSeconds(durationOfBreak)));

            services.AddHttpClient<UserService>(config =>
            {
                config.BaseAddress = new Uri(configuration.GetSection(baseUrl)["PixSevenBackofficeApi"]);
                config.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            })
            .AddPolicyHandler(PollyExtensions.WaitAndRetry())
            .AddTransientHttpErrorPolicy(x => x.CircuitBreakerAsync(tryAlowedBeforeBreak, TimeSpan.FromSeconds(durationOfBreak)));

            return services;
        }
    }
}
