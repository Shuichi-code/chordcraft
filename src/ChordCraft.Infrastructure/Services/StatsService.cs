using ChordCraft.Core.DTOs.Progress;
using ChordCraft.Core.Interfaces;
using ChordCraft.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ChordCraft.Infrastructure.Services;

public class StatsService : IStatsService
{
    private readonly AppDbContext _db;
    public StatsService(AppDbContext db) => _db = db;

    public async Task<StatsDto> GetStatsAsync(Guid userId)
    {
        var attempts = await _db.LessonAttempts.Where(la => la.UserId == userId && la.CompletedAt != null).ToListAsync();
        var totalLessons = await _db.Lessons.CountAsync();
        var completedLessons = await _db.UserProgress.CountAsync(up => up.UserId == userId && up.BestStars >= 1);
        var overallAccuracy = attempts.Count > 0 ? attempts.Average(a => a.Accuracy) : 0m;
        var averageSpeed = attempts.Count > 0 ? attempts.Where(a => a.Speed > 0).DefaultIfEmpty().Average(a => a?.Speed ?? 0) : 0m;
        var coverage = totalLessons > 0 ? (decimal)completedLessons / totalLessons : 0m;
        var totalTime = TimeSpan.FromSeconds(attempts.Where(a => a.CompletedAt.HasValue).Sum(a => (a.CompletedAt!.Value - a.StartedAt).TotalSeconds));
        var dailyStats = attempts.GroupBy(a => DateOnly.FromDateTime(a.StartedAt)).OrderBy(g => g.Key)
            .Select(g => new DailyStatsDto(g.Key,
                TimeSpan.FromSeconds(g.Sum(a => a.CompletedAt.HasValue ? (a.CompletedAt.Value - a.StartedAt).TotalSeconds : 0)),
                g.Average(a => a.Accuracy),
                g.Where(a => a.Speed > 0).DefaultIfEmpty().Average(a => a?.Speed ?? 0)))
            .ToList();
        return new StatsDto(overallAccuracy, coverage, averageSpeed, totalTime, dailyStats, []);
    }
}
