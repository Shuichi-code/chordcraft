namespace ChordCraft.Client.Services;

public class ProgressRouterService : IClientProgressService
{
    private readonly AuthStateService _auth;
    private readonly LocalProgressService _local;
    private readonly ApiProgressService _api;

    public ProgressRouterService(AuthStateService auth, LocalProgressService local, ApiProgressService api)
    {
        _auth = auth;
        _local = local;
        _api = api;
    }

    private IClientProgressService Current => _auth.IsAuthenticated ? _api : _local;

    public Task RecordAttemptAsync(int lessonId, decimal accuracy, decimal speed, int stars)
        => Current.RecordAttemptAsync(lessonId, accuracy, speed, stars);

    public Task<int> GetBestStarsAsync(int lessonId)
        => Current.GetBestStarsAsync(lessonId);

    public Task<int> GetTotalStarsAsync()
        => Current.GetTotalStarsAsync();

    public Task<decimal> GetCompletionPercentAsync(int totalLessons)
        => Current.GetCompletionPercentAsync(totalLessons);

    public Task<bool> HasCompletedAsync(int lessonId)
        => Current.HasCompletedAsync(lessonId);
}
