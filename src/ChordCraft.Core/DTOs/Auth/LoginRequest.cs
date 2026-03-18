using System.ComponentModel.DataAnnotations;

namespace ChordCraft.Core.DTOs.Auth;

public record LoginRequest(
    [Required, EmailAddress] string Email,
    [Required] string Password);
