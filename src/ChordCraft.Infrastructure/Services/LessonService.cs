using ChordCraft.Core.DTOs.Lessons;
using ChordCraft.Core.Interfaces;
using ChordCraft.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ChordCraft.Infrastructure.Services;

public class LessonService : ILessonService
{
    private readonly AppDbContext _db;
    public LessonService(AppDbContext db) => _db = db;

    public async Task<List<PhaseDto>> GetAllPhasesAsync()
    {
        return await _db.Phases.OrderBy(p => p.Order)
            .Select(p => new PhaseDto(p.Id, p.Name, p.Description, p.Order,
                p.Lessons.OrderBy(l => l.Number)
                    .Select(l => new LessonSummaryDto(l.Id, l.Number, l.Title, l.Type, l.Description, l.SpeedGoal))
                    .ToList()))
            .ToListAsync();
    }

    public async Task<LessonDetailDto?> GetLessonAsync(int id)
    {
        var l = await _db.Lessons.FindAsync(id);
        if (l is null) return null;
        return new LessonDetailDto(l.Id, l.Number, l.Title, l.Type, l.Description, l.TargetInputs, l.PassAccuracyThreshold, l.SpeedGoal, l.Content);
    }

    public async Task<bool> IsLessonUnlockedAsync(int lessonId, Guid userId)
    {
        var lesson = await _db.Lessons.FindAsync(lessonId);
        if (lesson is null) return false;
        if (lesson.Number == 1) return true;
        var prev = await _db.Lessons.Where(l => l.Number == lesson.Number - 1).FirstOrDefaultAsync();
        if (prev is null) return true;
        var progress = await _db.UserProgress.FirstOrDefaultAsync(up => up.UserId == userId && up.LessonId == prev.Id);
        return progress is not null && progress.BestStars >= 1;
    }
}
