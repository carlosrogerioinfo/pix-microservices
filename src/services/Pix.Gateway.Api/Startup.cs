using Pix.Gateway.Api.Configurations;
using Pix.Microservices.Core.Extensions;
using Pix.Microservices.Core.HealthChecks;
using Pix.Microservices.Core.Middleware;
using Pix.Microservices.Core.Versioning;
using Serilog;
using System.Reflection;

namespace Pix.Gateway.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddWebApiConfiguration();

            services.AddSwaggerConfiguration();

            services.AddDependencyInjectionConfiguration(Configuration);

            services.AddApiVersioningConfiguration();

            services.AddMediatRConfiguration(Assembly.GetExecutingAssembly());

            services.AddHealthChecks();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

            app.UseSerilogRequestLogging(options =>
            {
                options.GetLevel = (ctx, elapsed, ex) =>
                {
                    if (ex != null || ctx.Response.StatusCode >= 500) return Serilog.Events.LogEventLevel.Error;
                    if (elapsed > 3000) return Serilog.Events.LogEventLevel.Warning;
                    return Serilog.Events.LogEventLevel.Information;
                };
            });

            app.UseWebApiConfiguration(true);

            app.UseHealthCheckConfiguration();

            app.UseSwaggerConfiguration(env);
        }
    }
}
