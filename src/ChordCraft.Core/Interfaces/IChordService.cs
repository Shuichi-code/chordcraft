using ChordCraft.Core.Entities;

namespace ChordCraft.Core.Interfaces;

public interface IChordService
{
    Task<List<ChordEntry>> GetChordsAsync(int? difficulty = null, string? category = null);
}
