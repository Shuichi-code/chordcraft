namespace ChordCraft.Core.DTOs.Badges;

public record BadgeDto(int Id, string Name, string Description, string IconUrl, bool Earned, DateTime? EarnedAt);
