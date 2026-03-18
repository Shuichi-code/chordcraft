using ChordCraft.Core.Enums;

namespace ChordCraft.Core.DTOs.Lessons;

public record LessonDetailDto(int Id, int Number, string Title, LessonType Type, string Description, string TargetInputs, decimal PassAccuracyThreshold, int? SpeedGoal, string Content);
