namespace ChordCraft.Core.DTOs.Lessons;

public record PhaseDto(int Id, string Name, string Description, int Order, List<LessonSummaryDto> Lessons);
