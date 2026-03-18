namespace ChordCraft.Client.Services;

public interface IClientProgressService
{
    Task RecordAttemptAsync(int lessonId, decimal accuracy, decimal speed, int stars);
    Task<int> GetBestStarsAsync(int lessonId);
    Task<int> GetTotalStarsAsync();
    Task<decimal> GetCompletionPercentAsync(int totalLessons);
    Task<bool> HasCompletedAsync(int lessonId);
}
