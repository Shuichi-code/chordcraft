using ChordCraft.Core.DTOs.Badges;
using ChordCraft.Core.DTOs.Progress;
using ChordCraft.Core.Entities;
using ChordCraft.Core.Interfaces;
using ChordCraft.Core.Scoring;
using ChordCraft.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ChordCraft.Infrastructure.Services;

public class ProgressService : IProgressService
{
    private readonly AppDbContext _db;
    private readonly IBadgeService _badgeService;

    public ProgressService(AppDbContext db, IBadgeService badgeService)
    {
        _db = db;
        _badgeService = badgeService;
    }

    public async Task<SubmitAttemptResponse> SubmitAttemptAsync(Guid userId, SubmitAttemptRequest request)
    {
        var lesson = await _db.Lessons.FindAsync(request.LessonId) ?? throw new ArgumentException("Lesson not found");
        var stars = CalculateStars(request.Accuracy, request.Speed, lesson);
        var points = CalculatePoints(stars, request.Accuracy, request.Speed, lesson);
        var passed = stars >= 1;

        _db.LessonAttempts.Add(new LessonAttempt
        {
            Id = Guid.NewGuid(), UserId = userId, LessonId = request.LessonId,
            StartedAt = DateTime.UtcNow.AddMinutes(-5), CompletedAt = DateTime.UtcNow,
            Accuracy = request.Accuracy, Speed = request.Speed,
            Stars = stars, Points = points, Passed = passed, InputLog = request.InputLog
        });

        var progress = await _db.UserProgress.FirstOrDefaultAsync(up => up.UserId == userId && up.LessonId == request.LessonId);
        if (progress is null)
        {
            _db.UserProgress.Add(new UserProgress
            {
                UserId = userId, LessonId = request.LessonId,
                BestStars = stars, BestAccuracy = request.Accuracy, BestSpeed = request.Speed,
                TotalAttempts = 1, FirstCompletedAt = passed ? DateTime.UtcNow : null, LastAttemptAt = DateTime.UtcNow
            });
        }
        else
        {
            progress.TotalAttempts++;
            progress.LastAttemptAt = DateTime.UtcNow;
            if (stars > progress.BestStars) progress.BestStars = stars;
            if (request.Accuracy > progress.BestAccuracy) progress.BestAccuracy = request.Accuracy;
            if (request.Speed > progress.BestSpeed) progress.BestSpeed = request.Speed;
            if (passed && progress.FirstCompletedAt is null) progress.FirstCompletedAt = DateTime.UtcNow;
        }

        await _db.SaveChangesAsync();
        List<BadgeDto>? newBadges = passed ? await _badgeService.EvaluateAndAwardBadgesAsync(userId) : null;
        return new SubmitAttemptResponse(stars, points, passed, newBadges);
    }

    public async Task<List<UserProgressDto>> GetAllProgressAsync(Guid userId)
    {
        return await _db.UserProgress.Where(up => up.UserId == userId)
            .Select(up => new UserProgressDto(up.LessonId, up.BestStars, up.BestAccuracy, up.BestSpeed, up.TotalAttempts))
            .ToListAsync();
    }

    public int CalculateStars(decimal accuracy, decimal speed, Lesson lesson) =>
        ScoringCalculator.CalculateStars(accuracy, speed, lesson.PassAccuracyThreshold, lesson.SpeedGoal);

    public int CalculatePoints(int stars, decimal accuracy, decimal speed, Lesson lesson) =>
        ScoringCalculator.CalculatePoints(stars, accuracy, speed, lesson.SpeedGoal, lesson.Type);
}
