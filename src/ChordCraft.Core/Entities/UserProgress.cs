namespace ChordCraft.Core.Entities;

public class UserProgress
{
    public Guid UserId { get; set; }
    public int LessonId { get; set; }
    public int BestStars { get; set; }
    public decimal BestAccuracy { get; set; }
    public decimal BestSpeed { get; set; }
    public int TotalAttempts { get; set; }
    public DateTime? FirstCompletedAt { get; set; }
    public DateTime LastAttemptAt { get; set; }
    public User User { get; set; } = null!;
    public Lesson Lesson { get; set; } = null!;
}
