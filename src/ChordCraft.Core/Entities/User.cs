using Microsoft.AspNetCore.Identity;

namespace ChordCraft.Core.Entities;

public class User : IdentityUser<Guid>
{
    public string? DisplayName { get; set; }
    public string? SessionId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime LastActiveAt { get; set; } = DateTime.UtcNow;

    public ICollection<LessonAttempt> Attempts { get; set; } = [];
    public ICollection<UserProgress> Progress { get; set; } = [];
    public ICollection<UserBadge> Badges { get; set; } = [];
}
