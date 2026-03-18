using ChordCraft.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChordCraft.Infrastructure.Data.Seed;

public class BadgeSeedData : IEntityTypeConfiguration<Badge>
{
    public void Configure(EntityTypeBuilder<Badge> builder)
    {
        builder.HasData(
            new Badge { Id = 1, Name = "First Steps", Description = "Complete your first lesson", IconUrl = "/images/badges/first-steps.svg", Criteria = """{"type":"first-lesson"}""" },
            new Badge { Id = 2, Name = "Switch Basics", Description = "Complete Phase 1", IconUrl = "/images/badges/switch-basics.svg", Criteria = """{"type":"phase-complete","phaseId":1}""" },
            new Badge { Id = 3, Name = "Direction Master", Description = "Complete Phase 2", IconUrl = "/images/badges/direction-master.svg", Criteria = """{"type":"phase-complete","phaseId":2}""" },
            new Badge { Id = 4, Name = "Speed Demon", Description = "Reach 20 WPM in a practice lesson", IconUrl = "/images/badges/speed-demon.svg", Criteria = """{"type":"speed-threshold","wpm":20}""" },
            new Badge { Id = 5, Name = "First Chord", Description = "Successfully type your first chord", IconUrl = "/images/badges/first-chord.svg", Criteria = """{"type":"first-chord"}""" },
            new Badge { Id = 6, Name = "Chord Collector", Description = "Learn 10 different chords", IconUrl = "/images/badges/chord-collector.svg", Criteria = """{"type":"chord-count","count":10}""" },
            new Badge { Id = 7, Name = "Chord Master", Description = "Learn 50 different chords", IconUrl = "/images/badges/chord-master.svg", Criteria = """{"type":"chord-count","count":50}""" },
            new Badge { Id = 8, Name = "5 Day Streak", Description = "Practice 5 days in a row", IconUrl = "/images/badges/5-day-streak.svg", Criteria = """{"type":"streak","days":5}""" },
            new Badge { Id = 9, Name = "Weekend Warrior", Description = "Practice on Saturday and Sunday", IconUrl = "/images/badges/weekend-warrior.svg", Criteria = """{"type":"weekend"}""" },
            new Badge { Id = 10, Name = "Marathon Runner", Description = "Practice for 30 minutes in one session", IconUrl = "/images/badges/marathon-runner.svg", Criteria = """{"type":"session-time","minutes":30}""" },
            new Badge { Id = 11, Name = "Perfectionist", Description = "Earn 5 stars on 10 lessons", IconUrl = "/images/badges/perfectionist.svg", Criteria = """{"type":"star-count","stars":5,"count":10}""" },
            new Badge { Id = 12, Name = "Full Layout", Description = "Show proficiency on all 90 CCOS inputs", IconUrl = "/images/badges/full-layout.svg", Criteria = """{"type":"full-layout"}""" },
            new Badge { Id = 13, Name = "The Heavyweight", Description = "Earn 5 stars on 5 lessons in one day", IconUrl = "/images/badges/heavyweight.svg", Criteria = """{"type":"daily-stars","stars":5,"count":5}""" },
            new Badge { Id = 14, Name = "Keyboard Crusher", Description = "Rack up 100 total lesson attempts", IconUrl = "/images/badges/keyboard-crusher.svg", Criteria = """{"type":"attempt-count","count":100}""" }
        );
    }
}
