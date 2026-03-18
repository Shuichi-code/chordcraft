using ChordCraft.Core.DTOs.Badges;

namespace ChordCraft.Core.Interfaces;

public interface IBadgeService
{
    Task<List<BadgeDto>> GetBadgesForUserAsync(Guid userId);
    Task<List<BadgeDto>> EvaluateAndAwardBadgesAsync(Guid userId);
}
