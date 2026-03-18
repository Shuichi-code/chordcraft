namespace ChordCraft.Core.Entities;

public class ChordEntry
{
    public int Id { get; set; }
    public string OutputText { get; set; } = string.Empty;
    public string InputKeys { get; set; } = "[]";
    public int Difficulty { get; set; }
    public string Category { get; set; } = string.Empty;
}
