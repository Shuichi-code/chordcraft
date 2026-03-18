namespace ChordCraft.Client.Models;

public class TypingState
{
    public int CurrentPosition { get; set; }
    public List<TypedCharacter> TypedCharacters { get; set; } = [];
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public bool IsComplete { get; set; }

    public decimal Accuracy => TypedCharacters.Count == 0 ? 0
        : (decimal)TypedCharacters.Count(c => c.IsCorrect) / TypedCharacters.Count;

    public decimal Speed
    {
        get
        {
            if (StartTime is null || EndTime is null || EndTime == StartTime) return 0;
            var minutes = (decimal)(EndTime.Value - StartTime.Value).TotalMinutes;
            if (minutes <= 0) return 0;
            var correctChars = TypedCharacters.Count(c => c.IsCorrect);
            return correctChars / 5m / minutes; // WPM = chars / 5 / minutes
        }
    }
}

public record TypedCharacter(char Expected, char Actual, bool IsCorrect, double TimestampMs);
