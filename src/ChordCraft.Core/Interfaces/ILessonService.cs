using ChordCraft.Core.DTOs.Lessons;

namespace ChordCraft.Core.Interfaces;

public interface ILessonService
{
    Task<List<PhaseDto>> GetAllPhasesAsync();
    Task<LessonDetailDto?> GetLessonAsync(int id);
    Task<bool> IsLessonUnlockedAsync(int lessonId, Guid userId);
}
