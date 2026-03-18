using ChordCraft.Core.Enums;

namespace ChordCraft.Core.DTOs.Lessons;

public record LessonSummaryDto(int Id, int Number, string Title, LessonType Type, string Description, int? SpeedGoal);
