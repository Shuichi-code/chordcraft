using ChordCraft.Core.Enums;
using ChordCraft.Core.Scoring;

namespace ChordCraft.Core.Tests;

public class ScoringEdgeCaseTests
{
    [Fact]
    public void CalculateStars_AccuracyJustBelowThreshold_ReturnsZero()
    {
        // 0.7999... is just below the 0.80 threshold
        var result = ScoringCalculator.CalculateStars(0.7999m, 30m, 0.80m, 20);
        Assert.Equal(0, result);
    }

    [Fact]
    public void CalculateStars_AccuracyExactlyAtThreshold_ReturnsAtLeastOne()
    {
        var result = ScoringCalculator.CalculateStars(0.80m, 5m, 0.80m, 20);
        Assert.True(result >= 1);
    }

    [Fact]
    public void CalculateStars_AccuracyExactlyAtThreshold_NoSpeedGoal_ReturnsOne()
    {
        var result = ScoringCalculator.CalculateStars(0.80m, 0m, 0.80m, null);
        Assert.Equal(1, result);
    }

    [Fact]
    public void CalculatePoints_SpeedZero_WithSpeedGoal_HandlesCorrectly()
    {
        // stars=1, (80-80)*2=0, min(0,20)/20*20=0 => 1*10+0+0 = 10
        var result = ScoringCalculator.CalculatePoints(1, 0.80m, 0m, 20, LessonType.Practice);
        Assert.Equal(10, result);
    }

    [Fact]
    public void CalculatePoints_SpeedExceedsGoal_IsCapped()
    {
        // Speed 40 exceeds goal 20; should use min(40, 20) = 20
        // stars=3, (90-80)*2=20, min(40,20)/20*20=20 => 30+20+20 = 70
        var result = ScoringCalculator.CalculatePoints(3, 0.90m, 40m, 20, LessonType.Practice);
        Assert.Equal(70, result);
    }

    [Fact]
    public void CalculatePoints_SpeedExactlyAtGoal_FullSpeedBonus()
    {
        // Speed exactly at goal: min(20, 20) / 20 * 20 = 20
        // stars=3, (90-80)*2=20, 20 => 30+20+20 = 70
        var result = ScoringCalculator.CalculatePoints(3, 0.90m, 20m, 20, LessonType.Practice);
        Assert.Equal(70, result);
    }

    [Fact]
    public void CalculatePoints_SpeedGoalNull_NoSpeedBonus()
    {
        // Without speed goal: stars=2, (86-80)*2=12 => 20+12 = 32
        var result = ScoringCalculator.CalculatePoints(2, 0.86m, 100m, null, LessonType.Practice);
        Assert.Equal(32, result);
    }

    [Fact]
    public void CalculatePoints_SpeedGoalZero_NoSpeedBonus()
    {
        // SpeedGoal = 0 means no speed component (guard against divide by zero)
        // stars=2, (86-80)*2=12 => 20+12 = 32
        var result = ScoringCalculator.CalculatePoints(2, 0.86m, 15m, 0, LessonType.Review);
        Assert.Equal(32, result);
    }
}
