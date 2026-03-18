namespace ChordCraft.Client.Models;

public class LocalProgressData
{
    public string SessionId { get; set; } = Guid.NewGuid().ToString();
    public Dictionary<int, LessonProgressEntry> Progress { get; set; } = new();
    public DateTime LastActiveAt { get; set; } = DateTime.UtcNow;
}

public class LessonProgressEntry
{
    public int BestStars { get; set; }
    public decimal BestAccuracy { get; set; }
    public decimal BestSpeed { get; set; }
    public int TotalAttempts { get; set; }
}
