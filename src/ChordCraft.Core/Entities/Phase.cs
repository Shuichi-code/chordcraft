namespace ChordCraft.Core.Entities;

public class Phase
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Order { get; set; }
    public ICollection<Lesson> Lessons { get; set; } = [];
}
