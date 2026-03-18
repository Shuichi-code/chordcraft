namespace ChordCraft.Core.DTOs.Auth;

public record AuthResponse(string AccessToken, string RefreshToken, DateTime ExpiresAt, string DisplayName);
