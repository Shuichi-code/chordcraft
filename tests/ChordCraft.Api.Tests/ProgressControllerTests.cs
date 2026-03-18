using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using ChordCraft.Core.DTOs.Auth;
using ChordCraft.Core.DTOs.Progress;

namespace ChordCraft.Api.Tests;

public class ProgressControllerTests : IClassFixture<CustomWebApplicationFactory>, IAsyncLifetime
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;
    private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };

    public ProgressControllerTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    public async Task InitializeAsync() => await _factory.EnsureSeededAsync();
    public Task DisposeAsync() => Task.CompletedTask;

    private async Task<string> GetAuthTokenAsync()
    {
        var email = $"progress_{Guid.NewGuid():N}@test.com";
        var regResponse = await _client.PostAsJsonAsync("/api/auth/register", new RegisterRequest(email, "Password123", "Progress User"));
        var auth = await regResponse.Content.ReadFromJsonAsync<AuthResponse>(JsonOptions);
        return auth!.AccessToken;
    }

    [Fact]
    public async Task SubmitAttempt_WithValidData_ReturnsStarsAndPoints()
    {
        var token = await GetAuthTokenAsync();
        // Use lesson 2 (Intro type, PassAccuracy=0.80, no SpeedGoal)
        var request = new HttpRequestMessage(HttpMethod.Post, "/api/attempts")
        {
            Content = JsonContent.Create(new SubmitAttemptRequest(2, 0.92m, 18m, null))
        };
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var body = await response.Content.ReadFromJsonAsync<SubmitAttemptResponse>(JsonOptions);
        Assert.NotNull(body);
        Assert.True(body.Stars >= 1);
        Assert.True(body.Points > 0);
        Assert.True(body.Passed);
    }

    [Fact]
    public async Task SubmitAttempt_WithoutAuth_Returns401()
    {
        var response = await _client.PostAsJsonAsync("/api/attempts",
            new SubmitAttemptRequest(2, 0.92m, 18m, null));

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetProgress_WithoutAuth_Returns401()
    {
        var response = await _client.GetAsync("/api/progress");

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetProgress_WithAuth_Returns200()
    {
        var token = await GetAuthTokenAsync();
        var request = new HttpRequestMessage(HttpMethod.Get, "/api/progress");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetStats_WithoutAuth_Returns401()
    {
        var response = await _client.GetAsync("/api/stats");

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetStats_WithAuth_Returns200()
    {
        var token = await GetAuthTokenAsync();
        var request = new HttpRequestMessage(HttpMethod.Get, "/api/stats");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
