using ChordCraft.Core.Entities;
using ChordCraft.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChordCraft.Infrastructure.Data.Seed;

public class LessonSeedData : IEntityTypeConfiguration<Lesson>
{
    public void Configure(EntityTypeBuilder<Lesson> builder)
    {
        builder.HasData(
            new Lesson { Id = 1, PhaseId = 1, Number = 1, Title = "Introduction to CharaChorder", Type = LessonType.Video, Description = "Learn what the CharaChorder One is and how to hold it", Content = """{"videoUrl":"/videos/intro.mp4","durationSeconds":120,"completionThreshold":0.9}""" },
            new Lesson { Id = 2, PhaseId = 1, Number = 2, Title = "Left Index Push: f", Type = LessonType.Intro, Description = "Push the left index switch to type f", TargetInputs = """["L_Index:Push"]""", Content = """{"steps":[{"instruction":"Push the left index switch down to type f","targetInput":"L_Index:Push","targetChar":"f"}]}""" },
            new Lesson { Id = 3, PhaseId = 1, Number = 3, Title = "Right Index Push: j", Type = LessonType.Intro, Description = "Push the right index switch to type j", TargetInputs = """["R_Index:Push"]""", Content = """{"steps":[{"instruction":"Push the right index switch down to type j","targetInput":"R_Index:Push","targetChar":"j"}]}""" },
            new Lesson { Id = 4, PhaseId = 1, Number = 4, Title = "Review: f & j", Type = LessonType.Review, Description = "Practice typing f and j", TargetInputs = """["L_Index:Push","R_Index:Push"]""", Content = """{"sequence":["f","j","f","j","f","f","j","j","f","j","j","f","f","j","f","j","j","f","j","f"]}""" },
            new Lesson { Id = 5, PhaseId = 1, Number = 5, Title = "Space (Left Thumb Push)", Type = LessonType.Intro, Description = "Push the left thumb switch for space", TargetInputs = """["L_Thumb:Push"]""", Content = """{"steps":[{"instruction":"Push the left thumb switch to type a space","targetInput":"L_Thumb:Push","targetChar":" "}]}""" },
            new Lesson { Id = 6, PhaseId = 1, Number = 6, Title = "Left Middle Push: d", Type = LessonType.Intro, Description = "Push the left middle switch to type d", TargetInputs = """["L_Middle:Push"]""", Content = """{"steps":[{"instruction":"Push the left middle switch down to type d","targetInput":"L_Middle:Push","targetChar":"d"}]}""" },
            new Lesson { Id = 7, PhaseId = 1, Number = 7, Title = "Right Middle Push: k", Type = LessonType.Intro, Description = "Push the right middle switch to type k", TargetInputs = """["R_Middle:Push"]""", Content = """{"steps":[{"instruction":"Push the right middle switch down to type k","targetInput":"R_Middle:Push","targetChar":"k"}]}""" },
            new Lesson { Id = 8, PhaseId = 1, Number = 8, Title = "Review: d & k", Type = LessonType.Review, Description = "Practice typing d and k", TargetInputs = """["L_Middle:Push","R_Middle:Push"]""", Content = """{"sequence":["d","k","d","k","d","d","k","k","d","k","k","d","d","k","d","k","k","d","k","d"]}""" },
            new Lesson { Id = 9, PhaseId = 1, Number = 9, Title = "Practice: f j d k", Type = LessonType.Practice, Description = "Practice all four keys together", TargetInputs = """["L_Index:Push","R_Index:Push","L_Middle:Push","R_Middle:Push"]""", SpeedGoal = 5, Content = """{"text":"f j d k f k d j f d k j d f j k f j d k","timeLimitSeconds":120}""" },
            new Lesson { Id = 10, PhaseId = 1, Number = 10, Title = "Left Ring Push: s", Type = LessonType.Intro, Description = "Push the left ring switch to type s", TargetInputs = """["L_Ring:Push"]""", Content = """{"steps":[{"instruction":"Push the left ring switch down to type s","targetInput":"L_Ring:Push","targetChar":"s"}]}""" },
            new Lesson { Id = 11, PhaseId = 1, Number = 11, Title = "Right Ring Push: l", Type = LessonType.Intro, Description = "Push the right ring switch to type l", TargetInputs = """["R_Ring:Push"]""", Content = """{"steps":[{"instruction":"Push the right ring switch down to type l","targetInput":"R_Ring:Push","targetChar":"l"}]}""" },
            new Lesson { Id = 12, PhaseId = 1, Number = 12, Title = "Left Pinky Push: a", Type = LessonType.Intro, Description = "Push the left pinky switch to type a", TargetInputs = """["L_Pinky:Push"]""", Content = """{"steps":[{"instruction":"Push the left pinky switch down to type a","targetInput":"L_Pinky:Push","targetChar":"a"}]}""" },
            new Lesson { Id = 13, PhaseId = 1, Number = 13, Title = "Right Pinky Push: ;", Type = LessonType.Intro, Description = "Push the right pinky switch to type ;", TargetInputs = """["R_Pinky:Push"]""", Content = """{"steps":[{"instruction":"Push the right pinky switch down to type ;","targetInput":"R_Pinky:Push","targetChar":";"}]}""" },
            new Lesson { Id = 14, PhaseId = 1, Number = 14, Title = "All Push Keys", Type = LessonType.Review, Description = "Review all finger push inputs", TargetInputs = """["L_Pinky:Push","L_Ring:Push","L_Middle:Push","L_Index:Push","R_Index:Push","R_Middle:Push","R_Ring:Push","R_Pinky:Push"]""", Content = """{"sequence":["a","s","d","f","j","k","l",";","f","j","a",";","s","l","d","k"]}""" },
            new Lesson { Id = 15, PhaseId = 1, Number = 15, Title = "Play: Push Keys", Type = LessonType.Play, Description = "Game challenge with push keys", TargetInputs = """["L_Pinky:Push","L_Ring:Push","L_Middle:Push","L_Index:Push","R_Index:Push","R_Middle:Push","R_Ring:Push","R_Pinky:Push"]""", SpeedGoal = 8, Content = """{"gameType":"falling-characters","timeLimitSeconds":60,"wordPool":["a","s","d","f","j","k","l",";"]}""" }
        );
    }
}
