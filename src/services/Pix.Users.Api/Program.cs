using Pix.Microservices.Core.Logging;
using Serilog;

namespace Pix.Users.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SerilogConfiguration.ConfigureBootstrapLogger();
            try
            {
                Log.Information("Starting Pix.Users.Api");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Pix.Users.Api terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog((ctx, services, config) =>
                    SerilogConfiguration.CreateLoggerConfiguration(ctx.Configuration, "Pix.Users.Api")
                        .ReadFrom.Services(services))
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
