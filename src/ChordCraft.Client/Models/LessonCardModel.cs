namespace ChordCraft.Client.Models;

public record LessonCardModel(
    int Number, string Title, string Status,
    int Stars, int MaxStars, string? Score, bool IsNext, bool IsLocked);
