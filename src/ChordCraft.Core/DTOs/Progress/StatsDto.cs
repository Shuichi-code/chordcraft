namespace ChordCraft.Core.DTOs.Progress;

public record StatsDto(decimal OverallAccuracy, decimal Coverage, decimal AverageSpeed, TimeSpan TotalPracticeTime, List<DailyStatsDto> DailyStats, List<SwitchAccuracyDto> SwitchAccuracies);
public record DailyStatsDto(DateOnly Date, TimeSpan PracticeTime, decimal Accuracy, decimal Speed);
public record SwitchAccuracyDto(string SwitchId, string Direction, decimal Accuracy, int TotalInputs);
