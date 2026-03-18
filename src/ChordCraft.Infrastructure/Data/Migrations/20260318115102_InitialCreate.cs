using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ChordCraft.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DisplayName = table.Column<string>(type: "text", nullable: true),
                    SessionId = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastActiveAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Badges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IconUrl = table.Column<string>(type: "text", nullable: false),
                    Criteria = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Badges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChordEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OutputText = table.Column<string>(type: "text", nullable: false),
                    InputKeys = table.Column<string>(type: "jsonb", nullable: false),
                    Difficulty = table.Column<int>(type: "integer", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChordEntries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Phases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserBadges",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    BadgeId = table.Column<int>(type: "integer", nullable: false),
                    EarnedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBadges", x => new { x.UserId, x.BadgeId });
                    table.ForeignKey(
                        name: "FK_UserBadges_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserBadges_Badges_BadgeId",
                        column: x => x.BadgeId,
                        principalTable: "Badges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PhaseId = table.Column<int>(type: "integer", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    TargetInputs = table.Column<string>(type: "jsonb", nullable: false),
                    PassAccuracyThreshold = table.Column<decimal>(type: "numeric(5,4)", precision: 5, scale: 4, nullable: false),
                    SpeedGoal = table.Column<int>(type: "integer", nullable: true),
                    Content = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lessons_Phases_PhaseId",
                        column: x => x.PhaseId,
                        principalTable: "Phases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LessonAttempts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LessonId = table.Column<int>(type: "integer", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Accuracy = table.Column<decimal>(type: "numeric(5,4)", precision: 5, scale: 4, nullable: false),
                    Speed = table.Column<decimal>(type: "numeric(7,2)", precision: 7, scale: 2, nullable: false),
                    Stars = table.Column<int>(type: "integer", nullable: false),
                    Points = table.Column<int>(type: "integer", nullable: false),
                    Passed = table.Column<bool>(type: "boolean", nullable: false),
                    InputLog = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonAttempts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonAttempts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessonAttempts_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProgress",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LessonId = table.Column<int>(type: "integer", nullable: false),
                    BestStars = table.Column<int>(type: "integer", nullable: false),
                    BestAccuracy = table.Column<decimal>(type: "numeric(5,4)", precision: 5, scale: 4, nullable: false),
                    BestSpeed = table.Column<decimal>(type: "numeric(7,2)", precision: 7, scale: 2, nullable: false),
                    TotalAttempts = table.Column<int>(type: "integer", nullable: false),
                    FirstCompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastAttemptAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProgress", x => new { x.UserId, x.LessonId });
                    table.ForeignKey(
                        name: "FK_UserProgress_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProgress_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Badges",
                columns: new[] { "Id", "Criteria", "Description", "IconUrl", "Name" },
                values: new object[,]
                {
                    { 1, "{\"type\":\"first-lesson\"}", "Complete your first lesson", "/images/badges/first-steps.svg", "First Steps" },
                    { 2, "{\"type\":\"phase-complete\",\"phaseId\":1}", "Complete Phase 1", "/images/badges/switch-basics.svg", "Switch Basics" },
                    { 3, "{\"type\":\"phase-complete\",\"phaseId\":2}", "Complete Phase 2", "/images/badges/direction-master.svg", "Direction Master" },
                    { 4, "{\"type\":\"speed-threshold\",\"wpm\":20}", "Reach 20 WPM in a practice lesson", "/images/badges/speed-demon.svg", "Speed Demon" },
                    { 5, "{\"type\":\"first-chord\"}", "Successfully type your first chord", "/images/badges/first-chord.svg", "First Chord" },
                    { 6, "{\"type\":\"chord-count\",\"count\":10}", "Learn 10 different chords", "/images/badges/chord-collector.svg", "Chord Collector" },
                    { 7, "{\"type\":\"chord-count\",\"count\":50}", "Learn 50 different chords", "/images/badges/chord-master.svg", "Chord Master" },
                    { 8, "{\"type\":\"streak\",\"days\":5}", "Practice 5 days in a row", "/images/badges/5-day-streak.svg", "5 Day Streak" },
                    { 9, "{\"type\":\"weekend\"}", "Practice on Saturday and Sunday", "/images/badges/weekend-warrior.svg", "Weekend Warrior" },
                    { 10, "{\"type\":\"session-time\",\"minutes\":30}", "Practice for 30 minutes in one session", "/images/badges/marathon-runner.svg", "Marathon Runner" },
                    { 11, "{\"type\":\"star-count\",\"stars\":5,\"count\":10}", "Earn 5 stars on 10 lessons", "/images/badges/perfectionist.svg", "Perfectionist" },
                    { 12, "{\"type\":\"full-layout\"}", "Show proficiency on all 90 CCOS inputs", "/images/badges/full-layout.svg", "Full Layout" },
                    { 13, "{\"type\":\"daily-stars\",\"stars\":5,\"count\":5}", "Earn 5 stars on 5 lessons in one day", "/images/badges/heavyweight.svg", "The Heavyweight" },
                    { 14, "{\"type\":\"attempt-count\",\"count\":100}", "Rack up 100 total lesson attempts", "/images/badges/keyboard-crusher.svg", "Keyboard Crusher" }
                });

            migrationBuilder.InsertData(
                table: "Phases",
                columns: new[] { "Id", "Description", "Name", "Order" },
                values: new object[,]
                {
                    { 1, "Learn the push direction on all finger switches", "Switch Basics", 1 },
                    { 2, "Master all 4 directional inputs across all switches", "Directional Inputs", 2 },
                    { 3, "Build typing speed with character-by-character entry", "CCE Speed Building", 3 },
                    { 4, "Learn to press multiple switches simultaneously for whole words", "Introduction to Chording", 4 },
                    { 5, "Build chord vocabulary and speed for real-world proficiency", "Advanced Chording", 5 }
                });

            migrationBuilder.InsertData(
                table: "Lessons",
                columns: new[] { "Id", "Content", "Description", "Number", "PassAccuracyThreshold", "PhaseId", "SpeedGoal", "TargetInputs", "Title", "Type" },
                values: new object[,]
                {
                    { 1, "{\"videoUrl\":\"/videos/intro.mp4\",\"durationSeconds\":120,\"completionThreshold\":0.9}", "Learn what the CharaChorder One is and how to hold it", 1, 0.80m, 1, null, "[]", "Introduction to CharaChorder", 4 },
                    { 2, "{\"steps\":[{\"instruction\":\"Push the left index switch down to type f\",\"targetInput\":\"L_Index:Push\",\"targetChar\":\"f\"}]}", "Push the left index switch to type f", 2, 0.80m, 1, null, "[\"L_Index:Push\"]", "Left Index Push: f", 0 },
                    { 3, "{\"steps\":[{\"instruction\":\"Push the right index switch down to type j\",\"targetInput\":\"R_Index:Push\",\"targetChar\":\"j\"}]}", "Push the right index switch to type j", 3, 0.80m, 1, null, "[\"R_Index:Push\"]", "Right Index Push: j", 0 },
                    { 4, "{\"sequence\":[\"f\",\"j\",\"f\",\"j\",\"f\",\"f\",\"j\",\"j\",\"f\",\"j\",\"j\",\"f\",\"f\",\"j\",\"f\",\"j\",\"j\",\"f\",\"j\",\"f\"]}", "Practice typing f and j", 4, 0.80m, 1, null, "[\"L_Index:Push\",\"R_Index:Push\"]", "Review: f & j", 1 },
                    { 5, "{\"steps\":[{\"instruction\":\"Push the left thumb switch to type a space\",\"targetInput\":\"L_Thumb:Push\",\"targetChar\":\" \"}]}", "Push the left thumb switch for space", 5, 0.80m, 1, null, "[\"L_Thumb:Push\"]", "Space (Left Thumb Push)", 0 },
                    { 6, "{\"steps\":[{\"instruction\":\"Push the left middle switch down to type d\",\"targetInput\":\"L_Middle:Push\",\"targetChar\":\"d\"}]}", "Push the left middle switch to type d", 6, 0.80m, 1, null, "[\"L_Middle:Push\"]", "Left Middle Push: d", 0 },
                    { 7, "{\"steps\":[{\"instruction\":\"Push the right middle switch down to type k\",\"targetInput\":\"R_Middle:Push\",\"targetChar\":\"k\"}]}", "Push the right middle switch to type k", 7, 0.80m, 1, null, "[\"R_Middle:Push\"]", "Right Middle Push: k", 0 },
                    { 8, "{\"sequence\":[\"d\",\"k\",\"d\",\"k\",\"d\",\"d\",\"k\",\"k\",\"d\",\"k\",\"k\",\"d\",\"d\",\"k\",\"d\",\"k\",\"k\",\"d\",\"k\",\"d\"]}", "Practice typing d and k", 8, 0.80m, 1, null, "[\"L_Middle:Push\",\"R_Middle:Push\"]", "Review: d & k", 1 },
                    { 9, "{\"text\":\"f j d k f k d j f d k j d f j k f j d k\",\"timeLimitSeconds\":120}", "Practice all four keys together", 9, 0.80m, 1, 5, "[\"L_Index:Push\",\"R_Index:Push\",\"L_Middle:Push\",\"R_Middle:Push\"]", "Practice: f j d k", 2 },
                    { 10, "{\"steps\":[{\"instruction\":\"Push the left ring switch down to type s\",\"targetInput\":\"L_Ring:Push\",\"targetChar\":\"s\"}]}", "Push the left ring switch to type s", 10, 0.80m, 1, null, "[\"L_Ring:Push\"]", "Left Ring Push: s", 0 },
                    { 11, "{\"steps\":[{\"instruction\":\"Push the right ring switch down to type l\",\"targetInput\":\"R_Ring:Push\",\"targetChar\":\"l\"}]}", "Push the right ring switch to type l", 11, 0.80m, 1, null, "[\"R_Ring:Push\"]", "Right Ring Push: l", 0 },
                    { 12, "{\"steps\":[{\"instruction\":\"Push the left pinky switch down to type a\",\"targetInput\":\"L_Pinky:Push\",\"targetChar\":\"a\"}]}", "Push the left pinky switch to type a", 12, 0.80m, 1, null, "[\"L_Pinky:Push\"]", "Left Pinky Push: a", 0 },
                    { 13, "{\"steps\":[{\"instruction\":\"Push the right pinky switch down to type ;\",\"targetInput\":\"R_Pinky:Push\",\"targetChar\":\";\"}]}", "Push the right pinky switch to type ;", 13, 0.80m, 1, null, "[\"R_Pinky:Push\"]", "Right Pinky Push: ;", 0 },
                    { 14, "{\"sequence\":[\"a\",\"s\",\"d\",\"f\",\"j\",\"k\",\"l\",\";\",\"f\",\"j\",\"a\",\";\",\"s\",\"l\",\"d\",\"k\"]}", "Review all finger push inputs", 14, 0.80m, 1, null, "[\"L_Pinky:Push\",\"L_Ring:Push\",\"L_Middle:Push\",\"L_Index:Push\",\"R_Index:Push\",\"R_Middle:Push\",\"R_Ring:Push\",\"R_Pinky:Push\"]", "All Push Keys", 1 },
                    { 15, "{\"gameType\":\"falling-characters\",\"timeLimitSeconds\":60,\"wordPool\":[\"a\",\"s\",\"d\",\"f\",\"j\",\"k\",\"l\",\";\"]}", "Game challenge with push keys", 15, 0.80m, 1, 8, "[\"L_Pinky:Push\",\"L_Ring:Push\",\"L_Middle:Push\",\"L_Index:Push\",\"R_Index:Push\",\"R_Middle:Push\",\"R_Ring:Push\",\"R_Pinky:Push\"]", "Play: Push Keys", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LessonAttempts_LessonId",
                table: "LessonAttempts",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonAttempts_UserId",
                table: "LessonAttempts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_PhaseId",
                table: "Lessons",
                column: "PhaseId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBadges_BadgeId",
                table: "UserBadges",
                column: "BadgeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProgress_LessonId",
                table: "UserProgress",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProgress_UserId",
                table: "UserProgress",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ChordEntries");

            migrationBuilder.DropTable(
                name: "LessonAttempts");

            migrationBuilder.DropTable(
                name: "UserBadges");

            migrationBuilder.DropTable(
                name: "UserProgress");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Badges");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Lessons");

            migrationBuilder.DropTable(
                name: "Phases");
        }
    }
}
