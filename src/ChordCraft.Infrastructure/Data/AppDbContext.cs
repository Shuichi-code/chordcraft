using ChordCraft.Core.Entities;
using ChordCraft.Infrastructure.Data.Seed;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChordCraft.Infrastructure.Data;

public class AppDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Phase> Phases => Set<Phase>();
    public DbSet<Lesson> Lessons => Set<Lesson>();
    public DbSet<LessonAttempt> LessonAttempts => Set<LessonAttempt>();
    public DbSet<UserProgress> UserProgress => Set<UserProgress>();
    public DbSet<Badge> Badges => Set<Badge>();
    public DbSet<UserBadge> UserBadges => Set<UserBadge>();
    public DbSet<ChordEntry> ChordEntries => Set<ChordEntry>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Composite keys
        builder.Entity<UserProgress>().HasKey(up => new { up.UserId, up.LessonId });
        builder.Entity<UserBadge>().HasKey(ub => new { ub.UserId, ub.BadgeId });

        // JSON columns (PostgreSQL jsonb)
        builder.Entity<Lesson>().Property(l => l.Content).HasColumnType("jsonb");
        builder.Entity<Lesson>().Property(l => l.TargetInputs).HasColumnType("jsonb");
        builder.Entity<LessonAttempt>().Property(la => la.InputLog).HasColumnType("jsonb");
        builder.Entity<Badge>().Property(b => b.Criteria).HasColumnType("jsonb");
        builder.Entity<ChordEntry>().Property(ce => ce.InputKeys).HasColumnType("jsonb");

        // Indexes
        builder.Entity<LessonAttempt>().HasIndex(la => la.UserId);
        builder.Entity<LessonAttempt>().HasIndex(la => la.LessonId);
        builder.Entity<UserProgress>().HasIndex(up => up.UserId);

        // Decimal precision
        builder.Entity<Lesson>().Property(l => l.PassAccuracyThreshold).HasPrecision(5, 4);
        builder.Entity<LessonAttempt>().Property(la => la.Accuracy).HasPrecision(5, 4);
        builder.Entity<LessonAttempt>().Property(la => la.Speed).HasPrecision(7, 2);
        builder.Entity<UserProgress>().Property(up => up.BestAccuracy).HasPrecision(5, 4);
        builder.Entity<UserProgress>().Property(up => up.BestSpeed).HasPrecision(7, 2);

        // Relationships
        builder.Entity<Lesson>().HasOne(l => l.Phase).WithMany(p => p.Lessons).HasForeignKey(l => l.PhaseId);
        builder.Entity<LessonAttempt>().HasOne(la => la.User).WithMany(u => u.Attempts).HasForeignKey(la => la.UserId);
        builder.Entity<LessonAttempt>().HasOne(la => la.Lesson).WithMany(l => l.Attempts).HasForeignKey(la => la.LessonId);
        builder.Entity<UserProgress>().HasOne(up => up.User).WithMany(u => u.Progress).HasForeignKey(up => up.UserId);
        builder.Entity<UserProgress>().HasOne(up => up.Lesson).WithMany(l => l.UserProgress).HasForeignKey(up => up.LessonId);
        builder.Entity<UserBadge>().HasOne(ub => ub.User).WithMany(u => u.Badges).HasForeignKey(ub => ub.UserId);
        builder.Entity<UserBadge>().HasOne(ub => ub.Badge).WithMany(b => b.UserBadges).HasForeignKey(ub => ub.BadgeId);

        // Seed data
        new Seed.PhaseSeedData().Configure(builder.Entity<Phase>());
        new Seed.BadgeSeedData().Configure(builder.Entity<Badge>());
        new Seed.LessonSeedData().Configure(builder.Entity<Lesson>());
    }
}
