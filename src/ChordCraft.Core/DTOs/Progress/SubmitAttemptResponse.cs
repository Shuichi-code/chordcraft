using ChordCraft.Core.DTOs.Badges;

namespace ChordCraft.Core.DTOs.Progress;

public record SubmitAttemptResponse(int Stars, int Points, bool Passed, List<BadgeDto>? NewBadges);
