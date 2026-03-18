using ChordCraft.Core.Entities;
using ChordCraft.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ChordCraft.Api.Tests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly string _dbName = "TestDb_" + Guid.NewGuid().ToString("N");
    private bool _seeded;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // Use non-Development to skip db.Database.MigrateAsync() in Program.cs
        builder.UseEnvironment("Testing");

        builder.ConfigureServices(services =>
        {
            // Remove every descriptor that touches DbContext options or Npgsql
            var toRemove = services.Where(d =>
            {
                var st = d.ServiceType.FullName ?? "";
                var it = d.ImplementationType?.FullName ?? "";
                return st.Contains("DbContextOptions")
                    || st.Contains("Npgsql")
                    || it.Contains("Npgsql");
            }).ToList();
            foreach (var d in toRemove) services.Remove(d);

            // Register InMemory DbContext
            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase(_dbName));
        });
    }

    /// <summary>
    /// Ensures the InMemory database is created (applies HasData seed) and a test user exists.
    /// </summary>
    public async Task EnsureSeededAsync()
    {
        if (_seeded) return;

        using var scope = Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await db.Database.EnsureCreatedAsync();

        // Create a test user via Identity
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        if (await userManager.FindByEmailAsync(TestConstants.TestEmail) is null)
        {
            var user = new User
            {
                Id = TestConstants.TestUserId,
                UserName = TestConstants.TestEmail,
                Email = TestConstants.TestEmail,
                DisplayName = "Test User",
                CreatedAt = DateTime.UtcNow,
                LastActiveAt = DateTime.UtcNow
            };
            await userManager.CreateAsync(user, TestConstants.TestPassword);
        }

        _seeded = true;
    }
}

public static class TestConstants
{
    public static readonly Guid TestUserId = Guid.Parse("aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee");
    public const string TestEmail = "test@chordcraft.com";
    public const string TestPassword = "TestPass123";
}
