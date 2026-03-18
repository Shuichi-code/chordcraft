using ChordCraft.Core.Enums;

namespace ChordCraft.Core.Scoring;

public static class ScoringCalculator
{
    public static int CalculateStars(decimal accuracy, decimal speed, decimal passThreshold, int? speedGoal)
    {
        if (accuracy < passThreshold) return 0;

        if (speedGoal.HasValue)
        {
            var goal = (decimal)speedGoal.Value;
            if (accuracy >= 0.98m && speed >= goal) return 5;
            if (accuracy >= 0.95m && speed >= goal * 0.85m) return 4;
            if (accuracy >= 0.90m && speed >= goal * 0.70m) return 3;
            if (accuracy >= 0.85m && speed >= goal * 0.50m) return 2;
            return 1;
        }

        if (accuracy >= 0.98m) return 5;
        if (accuracy >= 0.95m) return 4;
        if (accuracy >= 0.90m) return 3;
        if (accuracy >= 0.85m) return 2;
        return 1;
    }

    public static int CalculatePoints(int stars, decimal accuracy, decimal speed, int? speedGoal, LessonType lessonType)
    {
        if (stars == 0) return 0;
        if (lessonType == LessonType.Video) return 10;

        var points = stars * 10m + (accuracy * 100m - 80m) * 2m;

        if (speedGoal.HasValue && speedGoal.Value > 0)
        {
            var speedRatio = Math.Min(speed, speedGoal.Value) / speedGoal.Value;
            points += speedRatio * 20m;
        }

        return Math.Min((int)Math.Round(points), 100);
    }
}
