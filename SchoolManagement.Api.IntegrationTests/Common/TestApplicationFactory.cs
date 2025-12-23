using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SchoolManagement.Infrastructure.Identity;
using SchoolManagement.Infrastructure.Persistence;

namespace SchoolManagement.Api.IntegrationTests.Common;

public class TestApplicationFactory : WebApplicationFactory<Program>
{
    private SqliteConnection _connection = null!;

    protected override IHost CreateHost(IHostBuilder builder)
    {
        // Configure test settings
        builder.ConfigureHostConfiguration(config =>
        {
            config.AddInMemoryCollection(new Dictionary<string, string>
            {
                ["Jwt:Key"] = "TestKeyThatIsAtLeast128BitsLongForTesting1234567890!",
                ["Jwt:Issuer"] = "TestIssuer",
                ["Jwt:Audience"] = "TestAudience",
                ["Jwt:ExpiryInMinutes"] = "60",
                ["ConnectionStrings:DefaultConnection"] = "DataSource=:memory:"
            });
        });

        return base.CreateHost(builder);
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureServices(services =>
        {
            // Remove existing DbContext
            var descriptors = services
                .Where(d => d.ServiceType == typeof(DbContextOptions<SchoolDbContext>))
                .ToList();

            foreach (var descriptor in descriptors)
                services.Remove(descriptor);

            // Create ONE shared SQLite connection
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            services.AddDbContext<SchoolDbContext>(options =>
            {
                options.UseSqlite(_connection);
            });

            // Build provider
            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<SchoolDbContext>();

            db.Database.EnsureCreated();

            IdentitySeeder.SeedRolesAsync(
                scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>())
                .GetAwaiter()
                .GetResult();
        });
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _connection?.Close();
            _connection?.Dispose();
        }
        base.Dispose(disposing);
    }
}