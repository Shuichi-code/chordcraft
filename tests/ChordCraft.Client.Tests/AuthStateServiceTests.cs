using ChordCraft.Client.Services;
using ChordCraft.Core.DTOs.Auth;
using Xunit;

namespace ChordCraft.Client.Tests;

public class FakeLocalStorageService : LocalStorageService
{
    private readonly Dictionary<string, string> _data = new();

    public FakeLocalStorageService() : base(null!) { }

    public override Task SetAsync(string key, string value)
    {
        _data[key] = value;
        return Task.CompletedTask;
    }

    public override Task<string?> GetAsync(string key)
        => Task.FromResult(_data.TryGetValue(key, out var val) ? val : (string?)null);

    public override Task RemoveAsync(string key)
    {
        _data.Remove(key);
        return Task.CompletedTask;
    }
}

public class AuthStateServiceTests
{
    private static AuthStateService CreateService(out FakeLocalStorageService storage)
    {
        storage = new FakeLocalStorageService();
        return new AuthStateService(storage);
    }

    [Fact]
    public void InitialState_IsNotAuthenticated()
    {
        var svc = new AuthStateService(new FakeLocalStorageService());
        Assert.False(svc.IsAuthenticated);
        Assert.Null(svc.Token);
        Assert.Null(svc.DisplayName);
    }

    [Fact]
    public async Task SetToken_SetsIsAuthenticatedTrue()
    {
        var svc = new AuthStateService(new FakeLocalStorageService());
        await svc.SetTokenAsync(new AuthResponse("tok123", "ref", DateTime.UtcNow.AddHours(1), "Alice"));
        Assert.True(svc.IsAuthenticated);
        Assert.Equal("tok123", svc.Token);
        Assert.Equal("Alice", svc.DisplayName);
    }

    [Fact]
    public async Task Clear_AfterSetToken_SetsIsAuthenticatedFalse()
    {
        var svc = new AuthStateService(new FakeLocalStorageService());
        await svc.SetTokenAsync(new AuthResponse("tok", "ref", DateTime.UtcNow.AddHours(1), "Bob"));
        await svc.ClearAsync();
        Assert.False(svc.IsAuthenticated);
        Assert.Null(svc.Token);
    }

    [Fact]
    public async Task SetToken_PersistsToStorage()
    {
        var svc = CreateService(out var storage);
        await svc.SetTokenAsync(new AuthResponse("tok", "ref", DateTime.UtcNow.AddHours(1), "Charlie"));
        Assert.Equal("tok", await storage.GetAsync("chordcraft_token"));
        Assert.Equal("Charlie", await storage.GetAsync("chordcraft_display_name"));
    }

    [Fact]
    public async Task Clear_RemovesFromStorage()
    {
        var svc = CreateService(out var storage);
        await svc.SetTokenAsync(new AuthResponse("tok", "ref", DateTime.UtcNow.AddHours(1), "Dave"));
        await svc.ClearAsync();
        Assert.Null(await storage.GetAsync("chordcraft_token"));
        Assert.Null(await storage.GetAsync("chordcraft_display_name"));
    }

    [Fact]
    public async Task AuthStateChanged_FiredOnSetToken()
    {
        var svc = new AuthStateService(new FakeLocalStorageService());
        var fired = false;
        svc.AuthStateChanged += () => fired = true;
        await svc.SetTokenAsync(new AuthResponse("tok", "ref", DateTime.UtcNow.AddHours(1), "Eve"));
        Assert.True(fired);
    }
}
