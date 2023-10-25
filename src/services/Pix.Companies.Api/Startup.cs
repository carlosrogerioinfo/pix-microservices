using Microsoft.EntityFrameworkCore;
using Pix.Companies.Api.Configurations;
using Pix.Microservices.Core.Jwt.Configuration;
using Pix.Microservices.Core.Jwt.Settings;
using Pix.Microservices.Infrastructure.Contexts;

namespace Pix.Companies.Api
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

            services.AddJWTBearerConfiguration(Configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseJWTBearerConfiguration();

            app.UseWebApiConfiguration(true);

            app.UseSwaggerConfiguration(env);
        }

        private void AddDataContextConfigurations(IServiceCollection services)
        {
            if (Configuration.GetSection("UseSqlServer").Value.ToLower().Trim() == "true")
            {
                services.AddDbContext<PixDataContext>(opt =>
                {
                    opt.UseSqlServer(Configuration.GetConnectionString("DatabaseConnection"));
                    opt.EnableSensitiveDataLogging();

                }, ServiceLifetime.Scoped);
            }
            else
            {
                services.AddDbContext<PixDataContext>(opt =>
                {
                    opt.UseNpgsql(Configuration.GetConnectionString("DatabaseConnection"));
                    opt.EnableSensitiveDataLogging();

                }, ServiceLifetime.Scoped);
            }

        }
    }
}
