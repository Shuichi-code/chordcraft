using ChordCraft.Core.DTOs.Auth;

namespace ChordCraft.Client.Services;

public class AuthStateService
{
    private readonly LocalStorageService _storage;
    private const string TokenKey = "chordcraft_token";
    private const string DisplayNameKey = "chordcraft_display_name";

    public event Action? AuthStateChanged;

    public string? Token { get; private set; }
    public string? DisplayName { get; private set; }
    public bool IsAuthenticated => !string.IsNullOrEmpty(Token);

    public AuthStateService(LocalStorageService storage) => _storage = storage;

    public async Task InitializeAsync()
    {
        Token = await _storage.GetAsync(TokenKey);
        DisplayName = await _storage.GetAsync(DisplayNameKey);
    }

    public async Task SetTokenAsync(AuthResponse response)
    {
        Token = response.AccessToken;
        DisplayName = response.DisplayName;
        await _storage.SetAsync(TokenKey, response.AccessToken);
        await _storage.SetAsync(DisplayNameKey, response.DisplayName);
        AuthStateChanged?.Invoke();
    }

    public async Task ClearAsync()
    {
        Token = null;
        DisplayName = null;
        await _storage.RemoveAsync(TokenKey);
        await _storage.RemoveAsync(DisplayNameKey);
        AuthStateChanged?.Invoke();
    }
}
