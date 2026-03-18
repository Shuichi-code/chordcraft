using System.Text.Json;
using ChordCraft.Core.DTOs.Badges;
using ChordCraft.Core.Entities;
using ChordCraft.Core.Interfaces;
using ChordCraft.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ChordCraft.Infrastructure.Services;

public class BadgeService : IBadgeService
{
    private readonly AppDbContext _db;
    public BadgeService(AppDbContext db) => _db = db;

    public async Task<List<BadgeDto>> GetBadgesForUserAsync(Guid userId)
    {
        var allBadges = await _db.Badges.ToListAsync();
        var earned = await _db.UserBadges.Where(ub => ub.UserId == userId).ToDictionaryAsync(ub => ub.BadgeId, ub => ub.EarnedAt);
        return allBadges.Select(b => new BadgeDto(b.Id, b.Name, b.Description, b.IconUrl, earned.ContainsKey(b.Id), earned.GetValueOrDefault(b.Id))).ToList();
    }

    public async Task<List<BadgeDto>> EvaluateAndAwardBadgesAsync(Guid userId)
    {
        var alreadyEarned = await _db.UserBadges.Where(ub => ub.UserId == userId).Select(ub => ub.BadgeId).ToHashSetAsync();
        var allBadges = await _db.Badges.ToListAsync();
        var newBadges = new List<BadgeDto>();

        foreach (var badge in allBadges.Where(b => !alreadyEarned.Contains(b.Id)))
        {
            if (await EvaluateBadge(badge, userId))
            {
                _db.UserBadges.Add(new UserBadge { UserId = userId, BadgeId = badge.Id, EarnedAt = DateTime.UtcNow });
                newBadges.Add(new BadgeDto(badge.Id, badge.Name, badge.Description, badge.IconUrl, true, DateTime.UtcNow));
            }
        }
        if (newBadges.Count > 0) await _db.SaveChangesAsync();
        return newBadges;
    }

    private async Task<bool> EvaluateBadge(Badge badge, Guid userId)
    {
        var criteria = JsonDocument.Parse(badge.Criteria).RootElement;
        var type = criteria.GetProperty("type").GetString();
        return type switch
        {
            "first-lesson" => await _db.LessonAttempts.AnyAsync(la => la.UserId == userId && la.Passed),
            "phase-complete" => await EvaluatePhaseComplete(criteria.GetProperty("phaseId").GetInt32(), userId),
            "speed-threshold" => await _db.LessonAttempts.AnyAsync(la => la.UserId == userId && la.Passed && la.Speed >= criteria.GetProperty("wpm").GetInt32()),
            "attempt-count" => await _db.LessonAttempts.CountAsync(la => la.UserId == userId) >= criteria.GetProperty("count").GetInt32(),
            "streak" => await EvaluateStreak(criteria.GetProperty("days").GetInt32(), userId),
            "star-count" => await _db.UserProgress.CountAsync(up => up.UserId == userId && up.BestStars == 5) >= criteria.GetProperty("count").GetInt32(),
            _ => false
        };
    }

    private async Task<bool> EvaluatePhaseComplete(int phaseId, Guid userId)
    {
        var lessonIds = await _db.Lessons.Where(l => l.PhaseId == phaseId).Select(l => l.Id).ToListAsync();
        var completed = await _db.UserProgress.CountAsync(up => up.UserId == userId && lessonIds.Contains(up.LessonId) && up.BestStars >= 1);
        return completed >= lessonIds.Count;
    }

    private async Task<bool> EvaluateStreak(int requiredDays, Guid userId)
    {
        var dates = await _db.LessonAttempts.Where(la => la.UserId == userId && la.Passed)
            .Select(la => la.CompletedAt!.Value.Date).Distinct().OrderByDescending(d => d).Take(requiredDays + 5).ToListAsync();
        if (dates.Count < requiredDays) return false;
        var streak = 1;
        for (int i = 1; i < dates.Count; i++)
        {
            if ((dates[i - 1] - dates[i]).Days == 1) { streak++; if (streak >= requiredDays) return true; }
            else streak = 1;
        }
        return streak >= requiredDays;
    }
}
