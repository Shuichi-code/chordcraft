using ChordCraft.Core.DTOs.Progress;

namespace ChordCraft.Core.Interfaces;

public interface IStatsService
{
    Task<StatsDto> GetStatsAsync(Guid userId);
}
