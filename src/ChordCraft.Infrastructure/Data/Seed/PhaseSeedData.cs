using ChordCraft.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChordCraft.Infrastructure.Data.Seed;

public class PhaseSeedData : IEntityTypeConfiguration<Phase>
{
    public void Configure(EntityTypeBuilder<Phase> builder)
    {
        builder.HasData(
            new Phase { Id = 1, Name = "Switch Basics", Description = "Learn the push direction on all finger switches", Order = 1 },
            new Phase { Id = 2, Name = "Directional Inputs", Description = "Master all 4 directional inputs across all switches", Order = 2 },
            new Phase { Id = 3, Name = "CCE Speed Building", Description = "Build typing speed with character-by-character entry", Order = 3 },
            new Phase { Id = 4, Name = "Introduction to Chording", Description = "Learn to press multiple switches simultaneously for whole words", Order = 4 },
            new Phase { Id = 5, Name = "Advanced Chording", Description = "Build chord vocabulary and speed for real-world proficiency", Order = 5 }
        );
    }
}
