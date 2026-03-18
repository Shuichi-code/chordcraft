using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using ChordCraft.Core.DTOs.Auth;
using ChordCraft.Core.DTOs.Progress;

namespace ChordCraft.Api.Tests;

public class AuthMigrateTests : IClassFixture<CustomWebApplicationFactory>, IAsyncLifetime
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;
    private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };

    public AuthMigrateTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    public async Task InitializeAsync() => await _factory.EnsureSeededAsync();
    public Task DisposeAsync() => Task.CompletedTask;

    [Fact]
    public async Task Login_WithValidCredentials_ReturnsToken()
    {
        var email = $"login_{Guid.NewGuid():N}@test.com";
        await _client.PostAsJsonAsync("/api/auth/register",
            new RegisterRequest(email, "Password1!", "LoginUser"));

        var resp = await _client.PostAsJsonAsync("/api/auth/login",
            new LoginRequest(email, "Password1!"));
        Assert.Equal(HttpStatusCode.OK, resp.StatusCode);
        var auth = await resp.Content.ReadFromJsonAsync<AuthResponse>(JsonOptions);
        Assert.NotNull(auth?.AccessToken);
    }

    [Fact]
    public async Task Login_WithInvalidCredentials_Returns401()
    {
        var resp = await _client.PostAsJsonAsync("/api/auth/login",
            new LoginRequest("nobody@test.com", "WrongPassword!"));
        Assert.Equal(HttpStatusCode.Unauthorized, resp.StatusCode);
    }

    [Fact]
    public async Task Register_ThenMigrate_Succeeds()
    {
        // Register
        var email = $"migrate_{Guid.NewGuid():N}@test.com";
        var registerReq = new RegisterRequest(email, "Password1!", "MigrateUser");
        var regResp = await _client.PostAsJsonAsync("/api/auth/register", registerReq);
        Assert.Equal(HttpStatusCode.OK, regResp.StatusCode);
        var auth = await regResp.Content.ReadFromJsonAsync<AuthResponse>(JsonOptions);
        Assert.NotNull(auth);

        // Set auth header
        _client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", auth!.AccessToken);

        // Migrate local progress
        var migrateResp = await _client.PostAsJsonAsync("/api/auth/migrate",
            new MigrateProgressRequest(
                $"session_{Guid.NewGuid():N}",
                new List<MigrateProgressEntry>
                {
                    new(1, 3, 0.90m, 15.0m, 2)
                },
                new List<MigrateLessonAttemptEntry>()));
        Assert.Equal(HttpStatusCode.OK, migrateResp.StatusCode);

        // Verify progress was stored
        var progressResp = await _client.GetAsync("/api/progress");
        Assert.Equal(HttpStatusCode.OK, progressResp.StatusCode);
        var progress = await progressResp.Content.ReadFromJsonAsync<List<UserProgressDto>>(JsonOptions);
        Assert.NotNull(progress);
        Assert.Contains(progress!, p => p.LessonId == 1 && p.BestStars == 3);
    }
}
