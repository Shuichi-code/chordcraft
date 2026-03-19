using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChordCraft.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixCC2Layout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Tilt the left index switch south (toward you) to type e\",\"targetInput\":\"L_Index:Down\",\"targetChar\":\"e\"}]}", "Tilt the left index switch south (toward you) to type e", "[\"L_Index:Down\"]", "Left Index South: e" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Tilt the right index switch south (toward you) to type t\",\"targetInput\":\"R_Index:Down\",\"targetChar\":\"t\"}]}", "Tilt the right index switch south (toward you) to type t", "[\"R_Index:Down\"]", "Right Index South: t" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"sequence\":[\"e\",\"t\",\"e\",\"t\",\"e\",\"e\",\"t\",\"t\",\"e\",\"t\",\"t\",\"e\",\"e\",\"t\",\"e\",\"t\",\"t\",\"e\",\"t\",\"e\"]}", "Practice typing e and t", "[\"L_Index:Down\",\"R_Index:Down\"]", "Review: e & t" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Tilt the right index switch west (to the left) to type a\",\"targetInput\":\"R_Index:Left\",\"targetChar\":\"a\"}]}", "Tilt the right index switch west (to the left) to type a", "[\"R_Index:Left\"]", "Right Index West: a" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Tilt the right middle switch south (toward you) to type s\",\"targetInput\":\"R_Middle:Down\",\"targetChar\":\"s\"}]}", "Tilt the right middle switch south (toward you) to type s", "[\"R_Middle:Down\"]", "Right Middle South: s" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"sequence\":[\"a\",\"s\",\"a\",\"s\",\"a\",\"a\",\"s\",\"s\",\"a\",\"s\",\"s\",\"a\",\"a\",\"s\",\"a\",\"s\",\"s\",\"a\",\"s\",\"a\"]}", "Practice typing a and s", "[\"R_Index:Left\",\"R_Middle:Down\"]", "Review: a & s" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Tilt the left middle switch south (toward you) to type o\",\"targetInput\":\"L_Middle:Down\",\"targetChar\":\"o\"}]}", "Tilt the left middle switch south (toward you) to type o", "[\"L_Middle:Down\"]", "Left Middle South: o" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Tilt the left index switch west (to the left) to type a space\",\"targetInput\":\"L_Index:Left\",\"targetChar\":\" \"}]}", "Tilt the left index switch west (to the left) to type a space", "[\"L_Index:Left\"]", "Left Index West: space" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"sequence\":[\"o\",\" \",\"t\",\"o\",\" \",\"e\",\"o\",\" \",\"s\",\"o\",\" \",\"a\",\" \",\"o\",\" \",\"t\",\"o\",\" \",\"e\",\" \"]}", "Practice o and space", "[\"L_Middle:Down\",\"L_Index:Left\"]", "Review: o & space" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Content", "Description", "Title" },
                values: new object[] { "{\"text\":\"eat sea oat toe sat tea ate set eat oat sea toe sat\",\"timeLimitSeconds\":120}", "Build speed with your first five keys", "Practice: e t a s o" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Tilt the left ring switch south (toward you) to type u\",\"targetInput\":\"L_Ring:Down\",\"targetChar\":\"u\"}]}", "Tilt the left ring switch south (toward you) to type u", "[\"L_Ring:Down\"]", "Left Ring South: u" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Tilt the right ring switch south (toward you) to type n\",\"targetInput\":\"R_Ring:Down\",\"targetChar\":\"n\"}]}", "Tilt the right ring switch south (toward you) to type n", "[\"R_Ring:Down\"]", "Right Ring South: n" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"sequence\":[\"u\",\"n\",\"u\",\"n\",\"u\",\"u\",\"n\",\"n\",\"u\",\"n\",\"n\",\"u\",\"u\",\"n\",\"u\",\"n\",\"n\",\"u\",\"n\",\"u\"]}", "Practice typing u and n", "[\"L_Ring:Down\",\"R_Ring:Down\"]", "Review: u & n" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "Content", "Description", "Title" },
                values: new object[] { "{\"gameType\":\"falling-characters\",\"timeLimitSeconds\":60,\"wordPool\":[\"e\",\"t\",\"a\",\"s\",\"o\",\"u\",\"n\"]}", "Game challenge with all learned keys", "Play: South & West Tilts" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 16,
                column: "Description",
                value: "Learn the north, east, and west tilt directions");

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Tilt the left index switch east (to the right) to type r\",\"targetInput\":\"L_Index:Right\",\"targetChar\":\"r\"}]}", "Tilt the left index switch east (to the right) to type r", "[\"L_Index:Right\"]", "Left Index East: r" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 18,
                column: "Description",
                value: "Tilt the left middle switch east (to the right) to type i");

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"sequence\":[\"r\",\"i\",\"r\",\"i\",\"i\",\"r\",\"r\",\"i\",\"r\",\"i\",\"i\",\"r\",\"r\",\"i\",\"r\",\"i\",\"i\",\"r\",\"i\",\"r\"]}", "Practice r and i", "[\"L_Index:Right\",\"L_Middle:Right\"]", "Review: r & i" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "Content", "Description" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Tilt the right middle switch north (away from you) to type y\",\"targetInput\":\"R_Middle:Up\",\"targetChar\":\"y\"}]}", "Tilt the right middle switch north (away from you) to type y" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Tilt the right middle switch west (to the left) to type j\",\"targetInput\":\"R_Middle:Left\",\"targetChar\":\"j\"}]}", "Tilt the right middle switch west (to the left) to type j", "[\"R_Middle:Left\"]", "Right Middle West: j" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"sequence\":[\"y\",\"j\",\"y\",\"j\",\"j\",\"y\",\"y\",\"j\",\"y\",\"j\",\"j\",\"y\",\"y\",\"j\",\"y\",\"j\",\"j\",\"y\",\"j\",\"y\"]}", "Practice y and j", "[\"R_Middle:Up\",\"R_Middle:Left\"]", "Review: y & j" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Tilt the right thumb switch south (toward you) to type d\",\"targetInput\":\"R_ThumbInner:Down\",\"targetChar\":\"d\"},{\"instruction\":\"Tilt the right thumb switch north (away from you) to type p\",\"targetInput\":\"R_ThumbInner:Up\",\"targetChar\":\"p\"}]}", "Tilt the right thumb switch south for d, north for p", "[\"R_ThumbInner:Down\",\"R_ThumbInner:Up\"]", "Right Thumb South & North: d & p" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Tilt the right thumb switch east (to the right) to type h\",\"targetInput\":\"R_ThumbInner:Right\",\"targetChar\":\"h\"},{\"instruction\":\"Tilt the right thumb switch west (to the left) to type f\",\"targetInput\":\"R_ThumbInner:Left\",\"targetChar\":\"f\"}]}", "Tilt the right thumb switch east for h, west for f", "[\"R_ThumbInner:Right\",\"R_ThumbInner:Left\"]", "Right Thumb East & West: h & f" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "Content", "Description", "TargetInputs", "Title", "Type" },
                values: new object[] { "{\"sequence\":[\"p\",\"h\",\"d\",\"f\",\"h\",\"p\",\"f\",\"d\",\"p\",\"f\",\"h\",\"d\",\"f\",\"p\",\"d\",\"h\",\"p\",\"d\",\"f\",\"h\"]}", "Practice all four right thumb tilt directions", "[\"R_ThumbInner:Up\",\"R_ThumbInner:Right\",\"R_ThumbInner:Down\",\"R_ThumbInner:Left\"]", "Review: p, h, d & f", 1 });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "Content", "Description", "TargetInputs", "Title", "Type" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Tilt the left thumb switch west (to the left) to type g\",\"targetInput\":\"L_ThumbInner:Left\",\"targetChar\":\"g\"},{\"instruction\":\"Tilt the left thumb switch east (to the right) to type w\",\"targetInput\":\"L_ThumbInner:Right\",\"targetChar\":\"w\"}]}", "Tilt the left thumb switch west for g, east for w", "[\"L_ThumbInner:Left\",\"L_ThumbInner:Right\"]", "Left Thumb West & East: g & w", 0 });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Tilt the left palm switch south (toward you) to type c\",\"targetInput\":\"L_PalmUpper:Down\",\"targetChar\":\"c\"}]}", "Tilt the left palm switch south (toward you) to type c", "[\"L_PalmUpper:Down\"]", "Left Palm South: c" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 28,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Tilt the left palm switch west (to the left) to type m\",\"targetInput\":\"L_PalmUpper:Left\",\"targetChar\":\"m\"}]}", "Tilt the left palm switch west (to the left) to type m", "[\"L_PalmUpper:Left\"]", "Left Palm West: m" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 29,
                columns: new[] { "Content", "Description", "SpeedGoal", "TargetInputs", "Title", "Type" },
                values: new object[] { "{\"text\":\"the man can run and eat food if it is hot the cat sat and ate\",\"timeLimitSeconds\":120}", "Speed drill with all letters learned so far", 6, "[]", "Practice: Common Letters", 2 });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 30,
                columns: new[] { "Content", "Description", "SpeedGoal", "TargetInputs", "Title", "Type" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Tilt the right ring switch west (to the left) to type l\",\"targetInput\":\"R_Ring:Left\",\"targetChar\":\"l\"}]}", "Tilt the right ring switch west (to the left) to type l", null, "[\"R_Ring:Left\"]", "Right Ring West: l", 0 });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 31,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Tilt the left palm switch east (to the right) to type k\",\"targetInput\":\"L_PalmUpper:Right\",\"targetChar\":\"k\"}]}", "Tilt the left palm switch east (to the right) to type k", "[\"L_PalmUpper:Right\"]", "Left Palm East: k" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Tilt the left palm switch north (away from you) to type v\",\"targetInput\":\"L_PalmUpper:Up\",\"targetChar\":\"v\"}]}", "Tilt the left palm switch north (away from you) to type v", "[\"L_PalmUpper:Up\"]", "Left Palm North: v" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "Content", "Description" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Tilt the left thumb switch south (toward you) to type z\",\"targetInput\":\"L_ThumbInner:Down\",\"targetChar\":\"z\"}]}", "Tilt the left thumb switch south (toward you) to type z" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Tilt the right palm switch north (away from you) to type x\",\"targetInput\":\"R_PalmUpper:Up\",\"targetChar\":\"x\"},{\"instruction\":\"Tilt the right palm switch west (to the left) to type b\",\"targetInput\":\"R_PalmUpper:Left\",\"targetChar\":\"b\"},{\"instruction\":\"Tilt the right palm switch south (toward you) to type q\",\"targetInput\":\"R_PalmUpper:Down\",\"targetChar\":\"q\"}]}", "Tilt the right palm switch for x, b, and q", "[\"R_PalmUpper:Up\",\"R_PalmUpper:Left\",\"R_PalmUpper:Down\"]", "Right Palm: x, b & q" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 35,
                column: "Content",
                value: "{\"gameType\":\"falling-characters\",\"timeLimitSeconds\":60,\"wordPool\":[\"r\",\"i\",\"y\",\"j\",\"d\",\"p\",\"h\",\"f\",\"g\",\"w\",\"c\",\"m\",\"l\",\"k\",\"v\",\"z\",\"x\",\"b\",\"q\"]}");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Press the left upper index switch straight down to type r\",\"targetInput\":\"L_Index:Push\",\"targetChar\":\"r\"}]}", "Press the left upper index switch straight down to type r", "[\"L_Index:Push\"]", "Left Index Press: r" });

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
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"sequence\":[\"o\",\"s\",\"o\",\"s\",\"o\",\"o\",\"s\",\"s\",\"o\",\"s\",\"s\",\"o\",\"o\",\"s\",\"o\",\"s\",\"s\",\"o\",\"s\",\"o\"]}", "Practice typing o and s", "[\"L_Middle:Push\",\"R_Middle:Push\"]", "Review: o & s" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Press the left lower index switch straight down to type e\",\"targetInput\":\"L_Thumb:Push\",\"targetChar\":\"e\"}]}", "Press the left lower index switch straight down to type e", "[\"L_Thumb:Push\"]", "Left Lower Index Press: e" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Press the right upper index switch straight down to type a\",\"targetInput\":\"R_Index:Push\",\"targetChar\":\"a\"}]}", "Press the right upper index switch straight down to type a", "[\"R_Index:Push\"]", "Right Upper Index Press: a" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"sequence\":[\"e\",\"a\",\"e\",\"a\",\"e\",\"e\",\"a\",\"a\",\"e\",\"a\",\"a\",\"e\",\"e\",\"a\",\"e\",\"a\",\"a\",\"e\",\"a\",\"e\"]}", "Practice typing e and a", "[\"L_Thumb:Push\",\"R_Index:Push\"]", "Review: e & a" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "Content", "Description", "Title" },
                values: new object[] { "{\"text\":\"r j o s e a r a e s o j r s a o j e r a\",\"timeLimitSeconds\":120}", "Build speed with your first six press keys", "Practice: r j o s e a" });

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
                columns: new[] { "Content", "Description", "Title" },
                values: new object[] { "{\"gameType\":\"falling-characters\",\"timeLimitSeconds\":60,\"wordPool\":[\"r\",\"j\",\"o\",\"s\",\"e\",\"a\",\"t\",\"w\"]}", "Game challenge with all learned press keys", "Play: Press Keys" });

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
                column: "Description",
                value: "Tilt the left middle switch east (right) to type i");

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
                columns: new[] { "Content", "Description" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Tilt the right middle switch north (forward, away from you) to type y\",\"targetInput\":\"R_Middle:Up\",\"targetChar\":\"y\"}]}", "Tilt the right middle switch north (forward) to type y" });

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
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"sequence\":[\"y\",\"n\",\"y\",\"n\",\"n\",\"y\",\"y\",\"n\",\"y\",\"n\",\"n\",\"y\",\"y\",\"n\",\"y\",\"n\",\"n\",\"y\",\"n\",\"y\"]}", "Practice y and n tilt inputs", "[\"R_Middle:Up\",\"R_Middle:Down\"]", "Review: y & n" });

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
                columns: new[] { "Content", "Description", "TargetInputs", "Title", "Type" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Tilt the right lower sub-switch west (to the left) to type f\",\"targetInput\":\"R_ThumbInner:Left\",\"targetChar\":\"f\"}]}", "Tilt the right lower switch west (left) to type f", "[\"R_ThumbInner:Left\"]", "Right Lower West: f", 0 });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "Content", "Description", "TargetInputs", "Title", "Type" },
                values: new object[] { "{\"sequence\":[\"h\",\"d\",\"f\",\"h\",\"d\",\"f\",\"d\",\"f\",\"h\",\"f\",\"h\",\"d\",\"h\",\"f\",\"d\",\"h\",\"d\",\"f\"]}", "Practice h, d, and f on the right lower sub-switch", "[\"R_ThumbInner:Right\",\"R_ThumbInner:Down\",\"R_ThumbInner:Left\"]", "Review: h, d & f", 1 });

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
                columns: new[] { "Content", "Description", "SpeedGoal", "TargetInputs", "Title", "Type" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Tilt the left lower sub-switch west (to the left) to type m\",\"targetInput\":\"L_PalmUpper:Left\",\"targetChar\":\"m\"}]}", "Tilt the left lower sub-switch west (left) to type m", null, "[\"L_PalmUpper:Left\"]", "Left Lower West: m", 0 });

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
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Tilt the right ring switch east (to the right) to type l\",\"targetInput\":\"R_Ring:Right\",\"targetChar\":\"l\"}]}", "Tilt the right ring switch east (right) to type l", "[\"R_Ring:Right\"]", "Right Ring East: l" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 32,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Tilt the left lower sub-switch east (to the right) to type k\",\"targetInput\":\"L_PalmUpper:Right\",\"targetChar\":\"k\"}]}", "Tilt the left lower sub-switch east (right) to type k", "[\"L_PalmUpper:Right\"]", "Left Lower East: k" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 33,
                columns: new[] { "Content", "Description" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Tilt the left thumb switch south (back toward you) to type z\",\"targetInput\":\"L_ThumbInner:Down\",\"targetChar\":\"z\"}]}", "Tilt the left thumb switch south (back) to type z" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 34,
                columns: new[] { "Content", "Description", "TargetInputs", "Title" },
                values: new object[] { "{\"steps\":[{\"instruction\":\"Tilt the right thumb switch north (forward) to type x\",\"targetInput\":\"R_PalmUpper:Up\",\"targetChar\":\"x\"},{\"instruction\":\"Tilt the right thumb switch south (back) to type q\",\"targetInput\":\"R_PalmUpper:Down\",\"targetChar\":\"q\"}]}", "Tilt the right thumb switch for x and q", "[\"R_PalmUpper:Up\",\"R_PalmUpper:Down\"]", "Right Thumb: x & q" });

            migrationBuilder.UpdateData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 35,
                column: "Content",
                value: "{\"gameType\":\"falling-characters\",\"timeLimitSeconds\":60,\"wordPool\":[\"u\",\"i\",\"y\",\"n\",\"h\",\"d\",\"f\",\"g\",\"c\",\"m\",\"l\",\"k\"]}");
        }
    }
}
