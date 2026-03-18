namespace ChordCraft.Core.DTOs.Progress;

public record UserProgressDto(int LessonId, int BestStars, decimal BestAccuracy, decimal BestSpeed, int TotalAttempts);
