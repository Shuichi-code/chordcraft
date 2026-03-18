using System.Text.Json;
using ChordCraft.Client.Models;
using ChordCraft.Core.Scoring;

namespace ChordCraft.Client.Services;

public class LocalProgressService
{
    private readonly LocalStorageService _storage;
    private const string StorageKey = "chordcraft_progress";
    private LocalProgressData? _cache;

    public LocalProgressService(LocalStorageService storage) => _storage = storage;

    public async Task<LocalProgressData> LoadAsync()
    {
        if (_cache is not null) return _cache;
        var json = await _storage.GetAsync(StorageKey);
        _cache = json is not null
            ? JsonSerializer.Deserialize<LocalProgressData>(json) ?? new()
            : new();
        return _cache;
    }

    public async Task SaveAsync()
    {
        if (_cache is null) return;
        _cache.LastActiveAt = DateTime.UtcNow;
        var json = JsonSerializer.Serialize(_cache);
        await _storage.SetAsync(StorageKey, json);
    }

    public async Task RecordAttemptAsync(int lessonId, decimal accuracy, decimal speed, int stars)
    {
        var data = await LoadAsync();
        if (data.Progress.TryGetValue(lessonId, out var entry))
        {
            entry.TotalAttempts++;
            if (stars > entry.BestStars) entry.BestStars = stars;
            if (accuracy > entry.BestAccuracy) entry.BestAccuracy = accuracy;
            if (speed > entry.BestSpeed) entry.BestSpeed = speed;
        }
        else
        {
            data.Progress[lessonId] = new LessonProgressEntry
            {
                BestStars = stars,
                BestAccuracy = accuracy,
                BestSpeed = speed,
                TotalAttempts = 1
            };
        }
        await SaveAsync();
    }

    public async Task<bool> IsLessonUnlockedAsync(int lessonNumber)
    {
        if (lessonNumber <= 1) return true;
        var data = await LoadAsync();
        // Find the lesson with Number = lessonNumber - 1
        // Since we only have IDs in progress, we need lesson number → ID mapping
        // This is handled by the catalog page which passes unlock state
        return true; // Simplified: catalog computes unlock from progress data
    }

    public async Task<int> GetTotalStarsAsync()
    {
        var data = await LoadAsync();
        return data.Progress.Values.Sum(p => p.BestStars);
    }

    public async Task<int> GetTotalPointsAsync()
    {
        var data = await LoadAsync();
        return data.Progress.Values.Sum(p => p.BestStars * 10); // Simplified
    }

    public async Task<decimal> GetCompletionPercentAsync(int totalLessons)
    {
        var data = await LoadAsync();
        var completed = data.Progress.Values.Count(p => p.BestStars >= 1);
        return totalLessons > 0 ? (decimal)completed / totalLessons * 100 : 0;
    }
}
