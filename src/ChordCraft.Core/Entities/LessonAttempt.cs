namespace ChordCraft.Core.Entities;

public class LessonAttempt
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public int LessonId { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public decimal Accuracy { get; set; }
    public decimal Speed { get; set; }
    public int Stars { get; set; }
    public int Points { get; set; }
    public bool Passed { get; set; }
    public string? InputLog { get; set; }
    public User User { get; set; } = null!;
    public Lesson Lesson { get; set; } = null!;
}
