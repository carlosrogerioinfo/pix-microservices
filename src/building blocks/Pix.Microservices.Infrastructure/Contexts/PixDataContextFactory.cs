using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Pix.Microservices.Infrastructure.Contexts;

public class PixDataContextFactory : IDesignTimeDbContextFactory<PixDataContext>
{
    public PixDataContext CreateDbContext(string[] args)
    {
        var connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION");

        if (string.IsNullOrEmpty(connectionString))
        {
            var basePath = Directory.GetCurrentDirectory();
            // Try to find appsettings.json in parent directories (when run from Infrastructure project)
            if (!File.Exists(Path.Combine(basePath, "appsettings.json")))
            {
                // Look in services directories
                var usersApiPath = Path.GetFullPath(Path.Combine(basePath, "..", "..", "services", "Pix.Users.Api"));
                if (Directory.Exists(usersApiPath))
                    basePath = usersApiPath;
            }

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            connectionString = configuration.GetConnectionString("DatabaseConnection")
                ?? "Host=localhost;Port=5432;Database=pix_db;User Id=postgres;Password=postgres;";
        }

        var optionsBuilder = new DbContextOptionsBuilder<PixDataContext>();

        var useSqlServer = Environment.GetEnvironmentVariable("USE_SQL_SERVER")?.ToLower() == "true";

        if (useSqlServer)
            optionsBuilder.UseSqlServer(connectionString);
        else
            optionsBuilder.UseNpgsql(connectionString);

        return new PixDataContext(optionsBuilder.Options);
    }
}
