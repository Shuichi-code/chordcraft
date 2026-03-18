using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using ChordCraft.Core.DTOs.Auth;

namespace ChordCraft.Api.Tests;

public class AuthControllerTests : IClassFixture<CustomWebApplicationFactory>, IAsyncLifetime
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;
    private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };

    public AuthControllerTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    public async Task InitializeAsync() => await _factory.EnsureSeededAsync();
    public Task DisposeAsync() => Task.CompletedTask;

    [Fact]
    public async Task Register_WithValidData_Returns200AndJwt()
    {
        var request = new RegisterRequest($"new_{Guid.NewGuid():N}@test.com", "Password123", "New User");
        var response = await _client.PostAsJsonAsync("/api/auth/register", request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var body = await response.Content.ReadFromJsonAsync<AuthResponse>(JsonOptions);
        Assert.NotNull(body);
        Assert.False(string.IsNullOrEmpty(body.AccessToken));
        Assert.False(string.IsNullOrEmpty(body.RefreshToken));
        Assert.Equal("New User", body.DisplayName);
        Assert.True(body.ExpiresAt > DateTime.UtcNow);
    }

    [Fact]
    public async Task Register_WithDuplicateEmail_Returns400()
    {
        var email = $"dup_{Guid.NewGuid():N}@test.com";
        var request = new RegisterRequest(email, "Password123", "First");
        await _client.PostAsJsonAsync("/api/auth/register", request);

        var duplicate = new RegisterRequest(email, "Password456", "Second");
        var response = await _client.PostAsJsonAsync("/api/auth/register", duplicate);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Login_WithValidCredentials_Returns200AndJwt()
    {
        var email = $"login_{Guid.NewGuid():N}@test.com";
        await _client.PostAsJsonAsync("/api/auth/register", new RegisterRequest(email, "Password123", "Login User"));

        var response = await _client.PostAsJsonAsync("/api/auth/login", new LoginRequest(email, "Password123"));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var body = await response.Content.ReadFromJsonAsync<AuthResponse>(JsonOptions);
        Assert.NotNull(body);
        Assert.False(string.IsNullOrEmpty(body.AccessToken));
        Assert.Equal("Login User", body.DisplayName);
    }

    [Fact]
    public async Task Login_WithWrongPassword_Returns401()
    {
        var email = $"wrong_{Guid.NewGuid():N}@test.com";
        await _client.PostAsJsonAsync("/api/auth/register", new RegisterRequest(email, "Password123", "User"));

        var response = await _client.PostAsJsonAsync("/api/auth/login", new LoginRequest(email, "WrongPassword1"));

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Migrate_WhileAuthenticated_Returns200()
    {
        var email = $"migrate_{Guid.NewGuid():N}@test.com";
        var regResponse = await _client.PostAsJsonAsync("/api/auth/register", new RegisterRequest(email, "Password123", "Migrator"));
        var auth = await regResponse.Content.ReadFromJsonAsync<AuthResponse>(JsonOptions);

        var migrateRequest = new MigrateProgressRequest(
            "session-123",
            new List<MigrateProgressEntry>
            {
                new(1, 3, 0.92m, 18m, 2)
            },
            new List<MigrateLessonAttemptEntry>
            {
                new(1, DateTime.UtcNow.AddMinutes(-10), DateTime.UtcNow.AddMinutes(-5), 0.92m, 18m, 3, 50, true)
            });

        var request = new HttpRequestMessage(HttpMethod.Post, "/api/auth/migrate")
        {
            Content = JsonContent.Create(migrateRequest)
        };
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", auth!.AccessToken);

        var response = await _client.SendAsync(request);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
