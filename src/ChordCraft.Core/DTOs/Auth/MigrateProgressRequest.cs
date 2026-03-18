namespace ChordCraft.Core.DTOs.Auth;

public record MigrateProgressRequest(string SessionId, List<MigrateProgressEntry> Progress, List<MigrateLessonAttemptEntry> Attempts);
public record MigrateProgressEntry(int LessonId, int BestStars, decimal BestAccuracy, decimal BestSpeed, int TotalAttempts);
public record MigrateLessonAttemptEntry(int LessonId, DateTime StartedAt, DateTime? CompletedAt, decimal Accuracy, decimal Speed, int Stars, int Points, bool Passed);
