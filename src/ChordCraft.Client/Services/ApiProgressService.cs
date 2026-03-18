using System.Net.Http.Headers;
using System.Net.Http.Json;
using ChordCraft.Core.DTOs.Progress;

namespace ChordCraft.Client.Services;

public class ApiProgressService : IClientProgressService
{
    private readonly HttpClient _http;
    private readonly AuthStateService _auth;
    private List<UserProgressDto>? _cache;

    public ApiProgressService(HttpClient http, AuthStateService auth)
    {
        _http = http;
        _auth = auth;
    }

    private void SetAuthHeader()
    {
        _http.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _auth.Token);
    }

    private async Task<List<UserProgressDto>> GetCacheAsync()
    {
        if (_cache is not null) return _cache;
        SetAuthHeader();
        _cache = await _http.GetFromJsonAsync<List<UserProgressDto>>("api/progress") ?? [];
        return _cache;
    }

    public async Task RecordAttemptAsync(int lessonId, decimal accuracy, decimal speed, int stars)
    {
        SetAuthHeader();
        var request = new SubmitAttemptRequest(lessonId, accuracy, speed, null);
        var response = await _http.PostAsJsonAsync("api/attempts", request);
        response.EnsureSuccessStatusCode();
        _cache = null; // invalidate
    }

    public async Task<int> GetBestStarsAsync(int lessonId)
    {
        var cache = await GetCacheAsync();
        return cache.FirstOrDefault(p => p.LessonId == lessonId)?.BestStars ?? 0;
    }

    public async Task<int> GetTotalStarsAsync()
    {
        var cache = await GetCacheAsync();
        return cache.Sum(p => p.BestStars);
    }

    public async Task<decimal> GetCompletionPercentAsync(int totalLessons)
    {
        var cache = await GetCacheAsync();
        var completed = cache.Count(p => p.BestStars >= 1);
        return totalLessons > 0 ? (decimal)completed / totalLessons * 100 : 0;
    }

    public async Task<bool> HasCompletedAsync(int lessonId)
    {
        var cache = await GetCacheAsync();
        return cache.Any(p => p.LessonId == lessonId && p.BestStars >= 1);
    }

    public void InvalidateCache() => _cache = null;
}
