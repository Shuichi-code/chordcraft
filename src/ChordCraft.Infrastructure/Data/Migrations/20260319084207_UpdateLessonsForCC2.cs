using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChordCraft.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLessonsForCC2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Title" },
                values: new object[] { "Learn what the CharaChorder 2 is and how to hold it", "Introduction to CharaChorder 2" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Content", "Description", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Press the left upper index switch straight down to type r\",\"targetInput\":\"L_Index:Push\",\"targetChar\":\"r\"}]}", "Press the left upper index switch straight down to type r", "Left Index Press: r" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Press the right ring switch straight down to type j\",\"targetInput\":\"R_Ring:Push\",\"targetChar\":\"j\"}]}", "Press the right ring switch straight down to type j", "[\"R_Ring:Push\"]", "Right Ring Press: j" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"sequence\":[\"r\",\"j\",\"r\",\"j\",\"r\",\"r\",\"j\",\"j\",\"r\",\"j\",\"j\",\"r\",\"r\",\"j\",\"r\",\"j\",\"j\",\"r\",\"j\",\"r\"]}", "Practice typing r and j", "[\"L_Index:Push\",\"R_Ring:Push\"]", "Review: r & j" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Press the left middle switch straight down to type o\",\"targetInput\":\"L_Middle:Push\",\"targetChar\":\"o\"}]}", "Press the left middle switch straight down to type o", "[\"L_Middle:Push\"]", "Left Middle Press: o" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Press the right middle switch straight down to type s\",\"targetInput\":\"R_Middle:Push\",\"targetChar\":\"s\"}]}", "Press the right middle switch straight down to type s", "[\"R_Middle:Push\"]", "Right Middle Press: s" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Content", "Description", "TargetInputs", "Title", "Type" },
                values: new object[] { "{\"sequence\":[\"o\",\"s\",\"o\",\"s\",\"o\",\"o\",\"s\",\"s\",\"o\",\"s\",\"s\",\"o\",\"o\",\"s\",\"o\",\"s\",\"s\",\"o\",\"s\",\"o\"]}", "Practice typing o and s", "[\"L_Middle:Push\",\"R_Middle:Push\"]", "Review: o & s", 1 });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Content", "Description", "TargetInputs", "Title", "Type" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Press the left lower index switch straight down to type e\",\"targetInput\":\"L_Thumb:Push\",\"targetChar\":\"e\"}]}", "Press the left lower index switch straight down to type e", "[\"L_Thumb:Push\"]", "Left Lower Index Press: e", 0 });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Content", "Description", "SpeedGoal", "TargetInputs", "Title", "Type" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Press the right upper index switch straight down to type a\",\"targetInput\":\"R_Index:Push\",\"targetChar\":\"a\"}]}", "Press the right upper index switch straight down to type a", null, "[\"R_Index:Push\"]", "Right Upper Index Press: a", 0 });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Content", "Description", "TargetInputs", "Title", "Type" },
                values: new object[] { "{\"sequence\":[\"e\",\"a\",\"e\",\"a\",\"e\",\"e\",\"a\",\"a\",\"e\",\"a\",\"a\",\"e\",\"e\",\"a\",\"e\",\"a\",\"a\",\"e\",\"a\",\"e\"]}", "Practice typing e and a", "[\"L_Thumb:Push\",\"R_Index:Push\"]", "Review: e & a", 1 });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Content", "Description", "SpeedGoal", "TargetInputs", "Title", "Type" },
                values: new object[] { "{\"text\":\"r j o s e a r a e s o j r s a o j e r a\",\"timeLimitSeconds\":120}", "Build speed with your first six press keys", 5, "[]", "Practice: r j o s e a", 2 });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Press the right lower index switch straight down to type t\",\"targetInput\":\"R_Thumb:Push\",\"targetChar\":\"t\"}]}", "Press the right lower index switch straight down to type t", "[\"R_Thumb:Push\"]", "Right Lower Index Press: t" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Press the left thumb switch straight down to type w\",\"targetInput\":\"L_ThumbInner:Push\",\"targetChar\":\"w\"}]}", "Press the left thumb switch straight down to type w", "[\"L_ThumbInner:Push\"]", "Left Thumb Press: w" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"sequence\":[\"t\",\"w\",\"t\",\"w\",\"t\",\"t\",\"w\",\"w\",\"t\",\"w\",\"w\",\"t\",\"t\",\"w\",\"t\",\"w\",\"w\",\"t\",\"w\",\"t\"]}", "Practice typing t and w", "[\"R_Thumb:Push\",\"L_ThumbInner:Push\"]", "Review: t & w" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"gameType\":\"falling-characters\",\"timeLimitSeconds\":60,\"wordPool\":[\"r\",\"j\",\"o\",\"s\",\"e\",\"a\",\"t\",\"w\"]}", "Game challenge with all learned press keys", "[]", "Play: Press Keys" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 16,
                column: "Description",
                value: "Learn the 4 tilt directions: north, south, east, and west");

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Tilt the left ring switch south (back toward you) to type u\",\"targetInput\":\"L_Ring:Down\",\"targetChar\":\"u\"}]}", "Tilt the left ring switch south (back) to type u", "[\"L_Ring:Down\"]", "Left Ring South: u" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Tilt the left middle switch east (to the right) to type i\",\"targetInput\":\"L_Middle:Right\",\"targetChar\":\"i\"}]}", "Tilt the left middle switch east (right) to type i", "[\"L_Middle:Right\"]", "Left Middle East: i" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"sequence\":[\"u\",\"i\",\"u\",\"i\",\"i\",\"u\",\"u\",\"i\",\"u\",\"i\",\"i\",\"u\",\"u\",\"i\",\"u\",\"i\",\"i\",\"u\",\"i\",\"u\"]}", "Practice u and i tilt inputs", "[\"L_Ring:Down\",\"L_Middle:Right\"]", "Review: u & i" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Tilt the right middle switch north (forward, away from you) to type y\",\"targetInput\":\"R_Middle:Up\",\"targetChar\":\"y\"}]}", "Tilt the right middle switch north (forward) to type y", "[\"R_Middle:Up\"]", "Right Middle North: y" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Tilt the right middle switch south (back toward you) to type n\",\"targetInput\":\"R_Middle:Down\",\"targetChar\":\"n\"}]}", "Tilt the right middle switch south (back) to type n", "[\"R_Middle:Down\"]", "Right Middle South: n" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Content", "Description", "TargetInputs", "Title", "Type" },
                values: new object[] { "{\"sequence\":[\"y\",\"n\",\"y\",\"n\",\"n\",\"y\",\"y\",\"n\",\"y\",\"n\",\"n\",\"y\",\"y\",\"n\",\"y\",\"n\",\"n\",\"y\",\"n\",\"y\"]}", "Practice y and n tilt inputs", "[\"R_Middle:Up\",\"R_Middle:Down\"]", "Review: y & n", 1 });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Tilt the right lower sub-switch east (to the right) to type h\",\"targetInput\":\"R_ThumbInner:Right\",\"targetChar\":\"h\"}]}", "Tilt the right lower switch east (right) to type h", "[\"R_ThumbInner:Right\"]", "Right Lower East: h" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Tilt the right lower sub-switch south (back toward you) to type d\",\"targetInput\":\"R_ThumbInner:Down\",\"targetChar\":\"d\"}]}", "Tilt the right lower switch south (back) to type d", "[\"R_ThumbInner:Down\"]", "Right Lower South: d" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Tilt the right lower sub-switch west (to the left) to type f\",\"targetInput\":\"R_ThumbInner:Left\",\"targetChar\":\"f\"}]}", "Tilt the right lower switch west (left) to type f", "[\"R_ThumbInner:Left\"]", "Right Lower West: f" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"sequence\":[\"h\",\"d\",\"f\",\"h\",\"d\",\"f\",\"d\",\"f\",\"h\",\"f\",\"h\",\"d\",\"h\",\"f\",\"d\",\"h\",\"d\",\"f\"]}", "Practice h, d, and f on the right lower sub-switch", "[\"R_ThumbInner:Right\",\"R_ThumbInner:Down\",\"R_ThumbInner:Left\"]", "Review: h, d & f" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Tilt the left thumb switch west (to the left) to type g\",\"targetInput\":\"L_ThumbInner:Left\",\"targetChar\":\"g\"}]}", "Tilt the left thumb switch west (left) to type g", "[\"L_ThumbInner:Left\"]", "Left Thumb West: g" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Tilt the left lower sub-switch south (back toward you) to type c\",\"targetInput\":\"L_PalmUpper:Down\",\"targetChar\":\"c\"}]}", "Tilt the left lower sub-switch south (back) to type c", "[\"L_PalmUpper:Down\"]", "Left Lower South: c" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Tilt the left lower sub-switch west (to the left) to type m\",\"targetInput\":\"L_PalmUpper:Left\",\"targetChar\":\"m\"}]}", "Tilt the left lower sub-switch west (left) to type m", "[\"L_PalmUpper:Left\"]", "Left Lower West: m" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "Content", "Description", "SpeedGoal", "TargetInputs", "Title", "Type" },
                values: new object[] { "{\"text\":\"e r i u o s a j e a r s i o u j r e s a u i o j\",\"timeLimitSeconds\":120}", "Speed drill: e r i u o s a j", 6, "[]", "Practice: vowels & common keys", 2 });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "Content", "Description", "SpeedGoal", "TargetInputs", "Title", "Type" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Tilt the right ring switch east (to the right) to type l\",\"targetInput\":\"R_Ring:Right\",\"targetChar\":\"l\"}]}", "Tilt the right ring switch east (right) to type l", null, "[\"R_Ring:Right\"]", "Right Ring East: l", 0 });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "Content", "Description", "SpeedGoal", "TargetInputs", "Title", "Type" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Tilt the left lower sub-switch east (to the right) to type k\",\"targetInput\":\"L_PalmUpper:Right\",\"targetChar\":\"k\"}]}", "Tilt the left lower sub-switch east (right) to type k", null, "[\"L_PalmUpper:Right\"]", "Left Lower East: k", 0 });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "Content", "Description", "TargetInputs", "Title", "Type" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Tilt the left thumb switch south (back toward you) to type z\",\"targetInput\":\"L_ThumbInner:Down\",\"targetChar\":\"z\"}]}", "Tilt the left thumb switch south (back) to type z", "[\"L_ThumbInner:Down\"]", "Left Thumb South: z", 0 });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "Content", "Description", "SpeedGoal", "TargetInputs", "Title", "Type" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Tilt the right thumb switch north (forward) to type x\",\"targetInput\":\"R_PalmUpper:Up\",\"targetChar\":\"x\"},{\"instruction\":\"Tilt the right thumb switch south (back) to type q\",\"targetInput\":\"R_PalmUpper:Down\",\"targetChar\":\"q\"}]}", "Tilt the right thumb switch for x and q", null, "[\"R_PalmUpper:Up\",\"R_PalmUpper:Down\"]", "Right Thumb: x & q", 0 });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "Content", "Description" },
                values: new object[] { "{\"gameType\":\"falling-characters\",\"timeLimitSeconds\":60,\"wordPool\":[\"u\",\"i\",\"y\",\"n\",\"h\",\"d\",\"f\",\"g\",\"c\",\"m\",\"l\",\"k\"]}", "Game challenge — all tilt directions" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Title" },
                values: new object[] { "Learn what the CharaChorder One is and how to hold it", "Introduction to CharaChorder" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Content", "Description", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Push the left index switch down to type f\",\"targetInput\":\"L_Index:Push\",\"targetChar\":\"f\"}]}", "Push the left index switch to type f", "Left Index Push: f" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Push the right index switch down to type j\",\"targetInput\":\"R_Index:Push\",\"targetChar\":\"j\"}]}", "Push the right index switch to type j", "[\"R_Index:Push\"]", "Right Index Push: j" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"sequence\":[\"f\",\"j\",\"f\",\"j\",\"f\",\"f\",\"j\",\"j\",\"f\",\"j\",\"j\",\"f\",\"f\",\"j\",\"f\",\"j\",\"j\",\"f\",\"j\",\"f\"]}", "Practice typing f and j", "[\"L_Index:Push\",\"R_Index:Push\"]", "Review: f & j" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Push the left thumb switch to type a space\",\"targetInput\":\"L_Thumb:Push\",\"targetChar\":\" \"}]}", "Push the left thumb switch for space", "[\"L_Thumb:Push\"]", "Space (Left Thumb Push)" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Push the left middle switch down to type d\",\"targetInput\":\"L_Middle:Push\",\"targetChar\":\"d\"}]}", "Push the left middle switch to type d", "[\"L_Middle:Push\"]", "Left Middle Push: d" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Content", "Description", "TargetInputs", "Title", "Type" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Push the right middle switch down to type k\",\"targetInput\":\"R_Middle:Push\",\"targetChar\":\"k\"}]}", "Push the right middle switch to type k", "[\"R_Middle:Push\"]", "Right Middle Push: k", 0 });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Content", "Description", "TargetInputs", "Title", "Type" },
                values: new object[] { "{\"sequence\":[\"d\",\"k\",\"d\",\"k\",\"d\",\"d\",\"k\",\"k\",\"d\",\"k\",\"k\",\"d\",\"d\",\"k\",\"d\",\"k\",\"k\",\"d\",\"k\",\"d\"]}", "Practice typing d and k", "[\"L_Middle:Push\",\"R_Middle:Push\"]", "Review: d & k", 1 });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Content", "Description", "SpeedGoal", "TargetInputs", "Title", "Type" },
                values: new object[] { "{\"text\":\"f j d k f k d j f d k j d f j k f j d k\",\"timeLimitSeconds\":120}", "Practice all four keys together", 5, "[\"L_Index:Push\",\"R_Index:Push\",\"L_Middle:Push\",\"R_Middle:Push\"]", "Practice: f j d k", 2 });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Content", "Description", "TargetInputs", "Title", "Type" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Push the left ring switch down to type s\",\"targetInput\":\"L_Ring:Push\",\"targetChar\":\"s\"}]}", "Push the left ring switch to type s", "[\"L_Ring:Push\"]", "Left Ring Push: s", 0 });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Content", "Description", "SpeedGoal", "TargetInputs", "Title", "Type" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Push the right ring switch down to type l\",\"targetInput\":\"R_Ring:Push\",\"targetChar\":\"l\"}]}", "Push the right ring switch to type l", null, "[\"R_Ring:Push\"]", "Right Ring Push: l", 0 });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Push the left pinky switch down to type a\",\"targetInput\":\"L_Pinky:Push\",\"targetChar\":\"a\"}]}", "Push the left pinky switch to type a", "[\"L_Pinky:Push\"]", "Left Pinky Push: a" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Push the right pinky switch down to type ;\",\"targetInput\":\"R_Pinky:Push\",\"targetChar\":\";\"}]}", "Push the right pinky switch to type ;", "[\"R_Pinky:Push\"]", "Right Pinky Push: ;" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"sequence\":[\"a\",\"s\",\"d\",\"f\",\"j\",\"k\",\"l\",\";\",\"f\",\"j\",\"a\",\";\",\"s\",\"l\",\"d\",\"k\"]}", "Review all finger push inputs", "[\"L_Pinky:Push\",\"L_Ring:Push\",\"L_Middle:Push\",\"L_Index:Push\",\"R_Index:Push\",\"R_Middle:Push\",\"R_Ring:Push\",\"R_Pinky:Push\"]", "All Push Keys" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"gameType\":\"falling-characters\",\"timeLimitSeconds\":60,\"wordPool\":[\"a\",\"s\",\"d\",\"f\",\"j\",\"k\",\"l\",\";\"]}", "Game challenge with push keys", "[\"L_Pinky:Push\",\"L_Ring:Push\",\"L_Middle:Push\",\"L_Index:Push\",\"R_Index:Push\",\"R_Middle:Push\",\"R_Ring:Push\",\"R_Pinky:Push\"]", "Play: Push Keys" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 16,
                column: "Description",
                value: "Learn the 4 directions: push, pull, east, and west");

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Lift the left index switch upward to type r\",\"targetInput\":\"L_Index:Pull\",\"targetChar\":\"r\"}]}", "Pull the left index switch up to type r", "[\"L_Index:Pull\"]", "Left Index Pull: r" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Lift the right index switch upward to type u\",\"targetInput\":\"R_Index:Pull\",\"targetChar\":\"u\"}]}", "Pull the right index switch up to type u", "[\"R_Index:Pull\"]", "Right Index Pull: u" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"sequence\":[\"r\",\"u\",\"r\",\"u\",\"u\",\"r\",\"r\",\"u\",\"r\",\"u\",\"u\",\"r\",\"r\",\"u\",\"r\",\"u\",\"u\",\"r\",\"u\",\"r\"]}", "Practice r and u pull inputs", "[\"L_Index:Pull\",\"R_Index:Pull\"]", "Review: r & u" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Lift the left middle switch upward to type e\",\"targetInput\":\"L_Middle:Pull\",\"targetChar\":\"e\"}]}", "Pull the left middle switch up to type e", "[\"L_Middle:Pull\"]", "Left Middle Pull: e" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Lift the right middle switch upward to type i\",\"targetInput\":\"R_Middle:Pull\",\"targetChar\":\"i\"}]}", "Pull the right middle switch up to type i", "[\"R_Middle:Pull\"]", "Right Middle Pull: i" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Content", "Description", "TargetInputs", "Title", "Type" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Lift the left ring switch upward to type w\",\"targetInput\":\"L_Ring:Pull\",\"targetChar\":\"w\"}]}", "Pull the left ring switch up to type w", "[\"L_Ring:Pull\"]", "Left Ring Pull: w", 0 });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Lift the right ring switch upward to type o\",\"targetInput\":\"R_Ring:Pull\",\"targetChar\":\"o\"}]}", "Pull the right ring switch up to type o", "[\"R_Ring:Pull\"]", "Right Ring Pull: o" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Lift the left pinky switch upward to type q\",\"targetInput\":\"L_Pinky:Pull\",\"targetChar\":\"q\"}]}", "Pull the left pinky switch up to type q", "[\"L_Pinky:Pull\"]", "Left Pinky Pull: q" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Lift the right pinky switch upward to type p\",\"targetInput\":\"R_Pinky:Pull\",\"targetChar\":\"p\"}]}", "Pull the right pinky switch up to type p", "[\"R_Pinky:Pull\"]", "Right Pinky Pull: p" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"sequence\":[\"r\",\"u\",\"e\",\"i\",\"w\",\"o\",\"q\",\"p\",\"e\",\"r\",\"i\",\"u\",\"w\",\"q\",\"o\",\"p\",\"r\",\"e\",\"u\",\"i\"]}", "Review all 8 pull inputs", "[\"L_Index:Pull\",\"R_Index:Pull\",\"L_Middle:Pull\",\"R_Middle:Pull\",\"L_Ring:Pull\",\"R_Ring:Pull\",\"L_Pinky:Pull\",\"R_Pinky:Pull\"]", "Review: All Pull Keys" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Tilt the left index switch to the right (east) to type t\",\"targetInput\":\"L_Index:East\",\"targetChar\":\"t\"},{\"instruction\":\"Tilt the left index switch to the left (west) to type g\",\"targetInput\":\"L_Index:West\",\"targetChar\":\"g\"}]}", "Tilt the left index east for t, west for g", "[\"L_Index:East\",\"L_Index:West\"]", "Index East/West: t & g" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Tilt the right index switch to the right (east) to type y\",\"targetInput\":\"R_Index:East\",\"targetChar\":\"y\"},{\"instruction\":\"Tilt the right index switch to the left (west) to type h\",\"targetInput\":\"R_Index:West\",\"targetChar\":\"h\"}]}", "Tilt the right index east for y, west for h", "[\"R_Index:East\",\"R_Index:West\"]", "Index East/West: y & h" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Tilt left middle east to type c\",\"targetInput\":\"L_Middle:East\",\"targetChar\":\"c\"},{\"instruction\":\"Tilt left middle west to type b\",\"targetInput\":\"L_Middle:West\",\"targetChar\":\"b\"},{\"instruction\":\"Tilt right middle east to type ,\",\"targetInput\":\"R_Middle:East\",\"targetChar\":\",\"},{\"instruction\":\"Tilt right middle west to type n\",\"targetInput\":\"R_Middle:West\",\"targetChar\":\"n\"}]}", "Learn east and west tilts on middle fingers", "[\"L_Middle:East\",\"L_Middle:West\",\"R_Middle:East\",\"R_Middle:West\"]", "Middle East/West: c, b, comma, n" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "Content", "Description", "SpeedGoal", "TargetInputs", "Title", "Type" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Tilt left ring east to type x\",\"targetInput\":\"L_Ring:East\",\"targetChar\":\"x\"},{\"instruction\":\"Tilt left ring west to type v\",\"targetInput\":\"L_Ring:West\",\"targetChar\":\"v\"},{\"instruction\":\"Tilt right ring east to type .\",\"targetInput\":\"R_Ring:East\",\"targetChar\":\".\"},{\"instruction\":\"Tilt right ring west to type m\",\"targetInput\":\"R_Ring:West\",\"targetChar\":\"m\"}]}", "Learn east and west tilts on ring and pinky fingers", null, "[\"L_Ring:East\",\"L_Ring:West\",\"R_Ring:East\",\"R_Ring:West\"]", "Ring & Pinky East/West", 0 });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "Content", "Description", "SpeedGoal", "TargetInputs", "Title", "Type" },
                values: new object[] { "{\"text\":\"e r i u e i r u e r u i r e u i e u r i\",\"timeLimitSeconds\":120}", "Speed drill on the most common pull inputs", 6, "[\"L_Middle:Pull\",\"L_Index:Pull\",\"R_Middle:Pull\",\"R_Index:Pull\"]", "Practice: e r i u", 2 });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "Content", "Description", "SpeedGoal", "TargetInputs", "Title", "Type" },
                values: new object[] { "{\"text\":\"t y g h t h y g t g h y g t y h t y g h\",\"timeLimitSeconds\":120}", "Speed drill on common east/west inputs", 5, "[\"L_Index:East\",\"R_Index:East\",\"L_Index:West\",\"R_Index:West\"]", "Practice: t y g h", 2 });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "Content", "Description", "TargetInputs", "Title", "Type" },
                values: new object[] { "{\"sequence\":[\"f\",\"r\",\"t\",\"g\",\"j\",\"u\",\"y\",\"h\",\"f\",\"u\",\"r\",\"j\",\"t\",\"y\",\"g\",\"h\"]}", "Mixed review of push, pull, east, and west", "[\"L_Index:Push\",\"L_Index:Pull\",\"L_Index:East\",\"L_Index:West\",\"R_Index:Push\",\"R_Index:Pull\",\"R_Index:East\",\"R_Index:West\"]", "Review: All 4 Directions", 1 });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "Content", "Description", "SpeedGoal", "TargetInputs", "Title", "Type" },
                values: new object[] { "{\"text\":\"fruit they grow hard if you are wet\",\"timeLimitSeconds\":90}", "Build speed across all directions", 8, "[]", "Speed Drill: All Directions", 2 });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 35,
                columns: new[] { "Content", "Description" },
                values: new object[] { "{\"gameType\":\"falling-characters\",\"timeLimitSeconds\":60,\"wordPool\":[\"r\",\"u\",\"e\",\"i\",\"w\",\"o\",\"t\",\"y\",\"g\",\"h\",\"c\",\"b\"]}", "Game challenge — hit all 4 directions" });
        }
    }
}
