using ChordCraft.Core.Enums;

namespace ChordCraft.Core.Entities;

public class Lesson
{
    public int Id { get; set; }
    public int PhaseId { get; set; }
    public int Number { get; set; }
    public string Title { get; set; } = string.Empty;
    public LessonType Type { get; set; }
    public string Description { get; set; } = string.Empty;
    public string TargetInputs { get; set; } = "[]";
    public decimal PassAccuracyThreshold { get; set; } = 0.80m;
    public int? SpeedGoal { get; set; }
    public string Content { get; set; } = "{}";
    public Phase Phase { get; set; } = null!;
    public ICollection<LessonAttempt> Attempts { get; set; } = [];
    public ICollection<UserProgress> UserProgress { get; set; } = [];
}
