
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration;
using CondoApp.Core.Interfaces;
using CondoApi.Infrastructure;

namespace CondoApp.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            // Load configuration from appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json", optional: true)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // Use the correct provider (PostgreSQL example)
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseNpgsql(connectionString);

            // Provide a dummy tenant provider for design-time only
            var tenantProvider = new DesignTimeTenantProvider();

            return new AppDbContext(optionsBuilder.Options, tenantProvider);
        }
    }

    // Dummy implementation for design-time migrations
    public class DesignTimeTenantProvider : ITenantProvider
    {
        public int GetTenantId() => 1; // or some sensible default for your setup
    }
}
