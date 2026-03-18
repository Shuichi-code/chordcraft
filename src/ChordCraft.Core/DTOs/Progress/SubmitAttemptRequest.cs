using System.ComponentModel.DataAnnotations;

namespace ChordCraft.Core.DTOs.Progress;

public record SubmitAttemptRequest([Required] int LessonId, [Range(0, 1)] decimal Accuracy, [Range(0, 500)] decimal Speed, string? InputLog);
