using ChordCraft.Core.Enums;
using ChordCraft.Core.Scoring;

namespace ChordCraft.Core.Tests;

public class ScoringTests
{
    [Theory]
    [InlineData(0.79, 30, 20, 0)]
    [InlineData(0.80, 5, 20, 1)]
    [InlineData(0.86, 11, 20, 2)]
    [InlineData(0.91, 15, 20, 3)]
    [InlineData(0.96, 18, 20, 4)]
    [InlineData(0.99, 21, 20, 5)]
    public void CalculateStars_WithSpeedGoal(double accuracy, double speed, int speedGoal, int expectedStars)
    {
        var result = ScoringCalculator.CalculateStars((decimal)accuracy, (decimal)speed, 0.80m, speedGoal);
        Assert.Equal(expectedStars, result);
    }

    [Theory]
    [InlineData(0.79, 0)]
    [InlineData(0.80, 1)]
    [InlineData(0.86, 2)]
    [InlineData(0.91, 3)]
    [InlineData(0.96, 4)]
    [InlineData(0.99, 5)]
    public void CalculateStars_WithoutSpeedGoal(double accuracy, int expectedStars)
    {
        var result = ScoringCalculator.CalculateStars((decimal)accuracy, 0m, 0.80m, null);
        Assert.Equal(expectedStars, result);
    }

    [Fact]
    public void CalculatePoints_VideoLesson_Returns10()
    {
        var result = ScoringCalculator.CalculatePoints(1, 0.90m, 0m, null, LessonType.Video);
        Assert.Equal(10, result);
    }

    [Fact]
    public void CalculatePoints_ZeroStars_ReturnsZero()
    {
        var result = ScoringCalculator.CalculatePoints(0, 0.70m, 10m, 20, LessonType.Practice);
        Assert.Equal(0, result);
    }

    [Fact]
    public void CalculatePoints_WithSpeedGoal_IncludesSpeedBonus()
    {
        // 3*10=30, (90-80)*2=20, min(15,20)/20*20=15 → 65
        var result = ScoringCalculator.CalculatePoints(3, 0.90m, 15m, 20, LessonType.Practice);
        Assert.Equal(65, result);
    }

    [Fact]
    public void CalculatePoints_WithoutSpeedGoal_NoSpeedBonus()
    {
        // 3*10=30, (90-80)*2=20 → 50
        var result = ScoringCalculator.CalculatePoints(3, 0.90m, 15m, null, LessonType.Review);
        Assert.Equal(50, result);
    }

    [Fact]
    public void CalculatePoints_CappedAt100()
    {
        var result = ScoringCalculator.CalculatePoints(5, 1.00m, 25m, 20, LessonType.Practice);
        Assert.Equal(100, result);
    }
}
