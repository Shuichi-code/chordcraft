using ChordCraft.Core.DTOs.Auth;

namespace ChordCraft.Client.Services;

public class AuthStateService
{
    private readonly LocalStorageService _storage;
    private const string TokenKey = "chordcraft_token";
    private const string DisplayNameKey = "chordcraft_display_name";
    private const string ExpiresAtKey = "chordcraft_expires_at";

    public event Action? AuthStateChanged;

    public string? Token { get; private set; }
    public string? DisplayName { get; private set; }
    public bool IsAuthenticated => !string.IsNullOrEmpty(Token);

    public AuthStateService(LocalStorageService storage) => _storage = storage;

    public async Task InitializeAsync()
    {
        var token = await _storage.GetAsync(TokenKey);
        var expiresAtRaw = await _storage.GetAsync(ExpiresAtKey);
        if (token is not null
            && DateTime.TryParse(expiresAtRaw, out var expiresAt)
            && expiresAt > DateTime.UtcNow)
        {
            Token = token;
            DisplayName = await _storage.GetAsync(DisplayNameKey);
        }
        else if (token is not null)
        {
            // Token exists but is expired — clear it
            await _storage.RemoveAsync(TokenKey);
            await _storage.RemoveAsync(DisplayNameKey);
            await _storage.RemoveAsync(ExpiresAtKey);
        }
    }

    public async Task SetTokenAsync(AuthResponse response)
    {
        Token = response.AccessToken;
        DisplayName = response.DisplayName;
        await _storage.SetAsync(TokenKey, response.AccessToken);
        await _storage.SetAsync(DisplayNameKey, response.DisplayName);
        await _storage.SetAsync(ExpiresAtKey, response.ExpiresAt.ToString("O"));
        AuthStateChanged?.Invoke();
    }

    public async Task ClearAsync()
    {
        Token = null;
        DisplayName = null;
        await _storage.RemoveAsync(TokenKey);
        await _storage.RemoveAsync(DisplayNameKey);
        await _storage.RemoveAsync(ExpiresAtKey);
        AuthStateChanged?.Invoke();
    }
}
