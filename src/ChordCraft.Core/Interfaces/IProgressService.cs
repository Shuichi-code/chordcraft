using ChordCraft.Core.DTOs.Progress;
using ChordCraft.Core.Entities;

namespace ChordCraft.Core.Interfaces;

public interface IProgressService
{
    Task<SubmitAttemptResponse> SubmitAttemptAsync(Guid userId, SubmitAttemptRequest request);
    Task<List<UserProgressDto>> GetAllProgressAsync(Guid userId);
    int CalculateStars(decimal accuracy, decimal speed, Lesson lesson);
    int CalculatePoints(int stars, decimal accuracy, decimal speed, Lesson lesson);
}
