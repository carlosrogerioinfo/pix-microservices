using Microsoft.EntityFrameworkCore;
using Pix.Users.Api.Configurations;
using Pix.Microservices.Core.Extensions;
using Pix.Microservices.Core.HealthChecks;
using Pix.Microservices.Core.Jwt.Configuration;
using Pix.Microservices.Core.Jwt.Settings;
using Pix.Microservices.Core.Middleware;
using Pix.Microservices.Core.Versioning;
using Pix.Microservices.Infrastructure.Contexts;
using Serilog;
using System.Reflection;

namespace Pix.Users.Api
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
            AddDataContextConfigurations(services);

            services.AddAutoMapperConfiguration();

            services.AddWebApiConfiguration();

            services.AddSwaggerConfiguration();

            services.AddDependencyInjectionConfiguration();

            var jwtSettings = Configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();
            var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");
            if (!string.IsNullOrEmpty(jwtSecret))
                jwtSettings.SecretKey = jwtSecret;
            services.AddJWTBearerConfiguration(jwtSettings);

            services.AddApiVersioningConfiguration();

            services.AddMediatRConfiguration(Assembly.GetExecutingAssembly());

            var connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION")
                ?? Configuration.GetConnectionString("DatabaseConnection");
            var useSqlServer = (Environment.GetEnvironmentVariable("UseSqlServer") ?? Configuration["UseSqlServer"] ?? "false").ToLower().Trim() == "true";

            var hcBuilder = services.AddHealthChecks();
            if (useSqlServer)
                hcBuilder.AddSqlServer(connectionString, name: "sqlserver", tags: new[] { "db", "ready" });
            else
                hcBuilder.AddNpgSql(connectionString, name: "postgresql", tags: new[] { "db", "ready" });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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

            app.UseJWTBearerConfiguration();

            app.UseWebApiConfiguration(true);

            app.UseHealthCheckConfiguration();

            app.UseSwaggerConfiguration(env);
        }

        private void AddDataContextConfigurations(IServiceCollection services)
        {
            var connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION")
                ?? Configuration.GetConnectionString("DatabaseConnection");
            var useSqlServer = (Environment.GetEnvironmentVariable("UseSqlServer") ?? Configuration.GetSection("UseSqlServer").Value ?? "false").ToLower().Trim() == "true";

            if (useSqlServer)
            {
                services.AddDbContext<PixDataContext>(opt =>
                {
                    opt.UseSqlServer(connectionString);
                    opt.EnableSensitiveDataLogging();
                }, ServiceLifetime.Scoped);
            }
            else
            {
                services.AddDbContext<PixDataContext>(opt =>
                {
                    opt.UseNpgsql(connectionString);
                    opt.EnableSensitiveDataLogging();
                }, ServiceLifetime.Scoped);
            }
        }
    }
}
