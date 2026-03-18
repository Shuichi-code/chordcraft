using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using ChordCraft.Core.DTOs.Auth;
using ChordCraft.Core.DTOs.Badges;

namespace ChordCraft.Api.Tests;

public class BadgesControllerTests : IClassFixture<CustomWebApplicationFactory>, IAsyncLifetime
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;
    private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };

    public BadgesControllerTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    public async Task InitializeAsync() => await _factory.EnsureSeededAsync();
    public Task DisposeAsync() => Task.CompletedTask;

    private async Task<string> GetAuthTokenAsync()
    {
        var email = $"badge_{Guid.NewGuid():N}@test.com";
        var regResponse = await _client.PostAsJsonAsync("/api/auth/register", new RegisterRequest(email, "Password123", "Badge User"));
        var auth = await regResponse.Content.ReadFromJsonAsync<AuthResponse>(JsonOptions);
        return auth!.AccessToken;
    }

    [Fact]
    public async Task GetBadges_WithoutAuth_Returns401()
    {
        var response = await _client.GetAsync("/api/badges");

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetBadges_WithAuth_ReturnsAllBadgesWithEarnedStatus()
    {
        var token = await GetAuthTokenAsync();
        var request = new HttpRequestMessage(HttpMethod.Get, "/api/badges");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _client.SendAsync(request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var badges = await response.Content.ReadFromJsonAsync<List<BadgeDto>>(JsonOptions);
        Assert.NotNull(badges);
        Assert.True(badges.Count >= 3);
        Assert.All(badges, b =>
        {
            Assert.False(string.IsNullOrEmpty(b.Name));
            Assert.False(string.IsNullOrEmpty(b.Description));
        });
    }
}
