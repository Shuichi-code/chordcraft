using System.ComponentModel.DataAnnotations;

namespace ChordCraft.Core.DTOs.Auth;

public record RegisterRequest(
    [Required, EmailAddress] string Email,
    [Required, MinLength(8)] string Password,
    [Required, MaxLength(50)] string DisplayName);
