using Pix.Microservices.Core.Logging;
using Serilog;

namespace Pix.Devices.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SerilogConfiguration.ConfigureBootstrapLogger();
            try
            {
                Log.Information("Starting Pix.Devices.Api");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Pix.Devices.Api terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog((ctx, services, config) =>
                    SerilogConfiguration.CreateLoggerConfiguration(ctx.Configuration, "Pix.Devices.Api")
                        .ReadFrom.Services(services))
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
