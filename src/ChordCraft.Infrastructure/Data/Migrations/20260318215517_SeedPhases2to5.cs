using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ChordCraft.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedPhases2to5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Lessons",
                columns: new[] { "Id", "Content", "Description", "Number", "PassAccuracyThreshold", "PhaseId", "SpeedGoal", "TargetInputs", "Title", "Type" },
                values: new object[,]
                {
                    { 16, "{\"videoUrl\":\"/videos/directions.mp4\",\"durationSeconds\":90,\"completionThreshold\":0.9}", "Learn the 4 directions: push, pull, east, and west", 1, 0.80m, 2, null, "[]", "Directions Explained", 4 },
                    { 17, "{\"steps\":[{\"instruction\":\"Lift the left index switch upward to type r\",\"targetInput\":\"L_Index:Pull\",\"targetChar\":\"r\"}]}", "Pull the left index switch up to type r", 2, 0.80m, 2, null, "[\"L_Index:Pull\"]", "Left Index Pull: r", 0 },
                    { 18, "{\"steps\":[{\"instruction\":\"Lift the right index switch upward to type u\",\"targetInput\":\"R_Index:Pull\",\"targetChar\":\"u\"}]}", "Pull the right index switch up to type u", 3, 0.80m, 2, null, "[\"R_Index:Pull\"]", "Right Index Pull: u", 0 },
                    { 19, "{\"sequence\":[\"r\",\"u\",\"r\",\"u\",\"u\",\"r\",\"r\",\"u\",\"r\",\"u\",\"u\",\"r\",\"r\",\"u\",\"r\",\"u\",\"u\",\"r\",\"u\",\"r\"]}", "Practice r and u pull inputs", 4, 0.80m, 2, null, "[\"L_Index:Pull\",\"R_Index:Pull\"]", "Review: r & u", 1 },
                    { 20, "{\"steps\":[{\"instruction\":\"Lift the left middle switch upward to type e\",\"targetInput\":\"L_Middle:Pull\",\"targetChar\":\"e\"}]}", "Pull the left middle switch up to type e", 5, 0.80m, 2, null, "[\"L_Middle:Pull\"]", "Left Middle Pull: e", 0 },
                    { 21, "{\"steps\":[{\"instruction\":\"Lift the right middle switch upward to type i\",\"targetInput\":\"R_Middle:Pull\",\"targetChar\":\"i\"}]}", "Pull the right middle switch up to type i", 6, 0.80m, 2, null, "[\"R_Middle:Pull\"]", "Right Middle Pull: i", 0 },
                    { 22, "{\"steps\":[{\"instruction\":\"Lift the left ring switch upward to type w\",\"targetInput\":\"L_Ring:Pull\",\"targetChar\":\"w\"}]}", "Pull the left ring switch up to type w", 7, 0.80m, 2, null, "[\"L_Ring:Pull\"]", "Left Ring Pull: w", 0 },
                    { 23, "{\"steps\":[{\"instruction\":\"Lift the right ring switch upward to type o\",\"targetInput\":\"R_Ring:Pull\",\"targetChar\":\"o\"}]}", "Pull the right ring switch up to type o", 8, 0.80m, 2, null, "[\"R_Ring:Pull\"]", "Right Ring Pull: o", 0 },
                    { 24, "{\"steps\":[{\"instruction\":\"Lift the left pinky switch upward to type q\",\"targetInput\":\"L_Pinky:Pull\",\"targetChar\":\"q\"}]}", "Pull the left pinky switch up to type q", 9, 0.80m, 2, null, "[\"L_Pinky:Pull\"]", "Left Pinky Pull: q", 0 },
                    { 25, "{\"steps\":[{\"instruction\":\"Lift the right pinky switch upward to type p\",\"targetInput\":\"R_Pinky:Pull\",\"targetChar\":\"p\"}]}", "Pull the right pinky switch up to type p", 10, 0.80m, 2, null, "[\"R_Pinky:Pull\"]", "Right Pinky Pull: p", 0 },
                    { 26, "{\"sequence\":[\"r\",\"u\",\"e\",\"i\",\"w\",\"o\",\"q\",\"p\",\"e\",\"r\",\"i\",\"u\",\"w\",\"q\",\"o\",\"p\",\"r\",\"e\",\"u\",\"i\"]}", "Review all 8 pull inputs", 11, 0.80m, 2, null, "[\"L_Index:Pull\",\"R_Index:Pull\",\"L_Middle:Pull\",\"R_Middle:Pull\",\"L_Ring:Pull\",\"R_Ring:Pull\",\"L_Pinky:Pull\",\"R_Pinky:Pull\"]", "Review: All Pull Keys", 1 },
                    { 27, "{\"steps\":[{\"instruction\":\"Tilt the left index switch to the right (east) to type t\",\"targetInput\":\"L_Index:East\",\"targetChar\":\"t\"},{\"instruction\":\"Tilt the left index switch to the left (west) to type g\",\"targetInput\":\"L_Index:West\",\"targetChar\":\"g\"}]}", "Tilt the left index east for t, west for g", 12, 0.80m, 2, null, "[\"L_Index:East\",\"L_Index:West\"]", "Index East/West: t & g", 0 },
                    { 28, "{\"steps\":[{\"instruction\":\"Tilt the right index switch to the right (east) to type y\",\"targetInput\":\"R_Index:East\",\"targetChar\":\"y\"},{\"instruction\":\"Tilt the right index switch to the left (west) to type h\",\"targetInput\":\"R_Index:West\",\"targetChar\":\"h\"}]}", "Tilt the right index east for y, west for h", 13, 0.80m, 2, null, "[\"R_Index:East\",\"R_Index:West\"]", "Index East/West: y & h", 0 },
                    { 29, "{\"steps\":[{\"instruction\":\"Tilt left middle east to type c\",\"targetInput\":\"L_Middle:East\",\"targetChar\":\"c\"},{\"instruction\":\"Tilt left middle west to type b\",\"targetInput\":\"L_Middle:West\",\"targetChar\":\"b\"},{\"instruction\":\"Tilt right middle east to type ,\",\"targetInput\":\"R_Middle:East\",\"targetChar\":\",\"},{\"instruction\":\"Tilt right middle west to type n\",\"targetInput\":\"R_Middle:West\",\"targetChar\":\"n\"}]}", "Learn east and west tilts on middle fingers", 14, 0.80m, 2, null, "[\"L_Middle:East\",\"L_Middle:West\",\"R_Middle:East\",\"R_Middle:West\"]", "Middle East/West: c, b, comma, n", 0 },
                    { 30, "{\"steps\":[{\"instruction\":\"Tilt left ring east to type x\",\"targetInput\":\"L_Ring:East\",\"targetChar\":\"x\"},{\"instruction\":\"Tilt left ring west to type v\",\"targetInput\":\"L_Ring:West\",\"targetChar\":\"v\"},{\"instruction\":\"Tilt right ring east to type .\",\"targetInput\":\"R_Ring:East\",\"targetChar\":\".\"},{\"instruction\":\"Tilt right ring west to type m\",\"targetInput\":\"R_Ring:West\",\"targetChar\":\"m\"}]}", "Learn east and west tilts on ring and pinky fingers", 15, 0.80m, 2, null, "[\"L_Ring:East\",\"L_Ring:West\",\"R_Ring:East\",\"R_Ring:West\"]", "Ring & Pinky East/West", 0 },
                    { 31, "{\"text\":\"e r i u e i r u e r u i r e u i e u r i\",\"timeLimitSeconds\":120}", "Speed drill on the most common pull inputs", 16, 0.80m, 2, 6, "[\"L_Middle:Pull\",\"L_Index:Pull\",\"R_Middle:Pull\",\"R_Index:Pull\"]", "Practice: e r i u", 2 },
                    { 32, "{\"text\":\"t y g h t h y g t g h y g t y h t y g h\",\"timeLimitSeconds\":120}", "Speed drill on common east/west inputs", 17, 0.80m, 2, 5, "[\"L_Index:East\",\"R_Index:East\",\"L_Index:West\",\"R_Index:West\"]", "Practice: t y g h", 2 },
                    { 33, "{\"sequence\":[\"f\",\"r\",\"t\",\"g\",\"j\",\"u\",\"y\",\"h\",\"f\",\"u\",\"r\",\"j\",\"t\",\"y\",\"g\",\"h\"]}", "Mixed review of push, pull, east, and west", 18, 0.80m, 2, null, "[\"L_Index:Push\",\"L_Index:Pull\",\"L_Index:East\",\"L_Index:West\",\"R_Index:Push\",\"R_Index:Pull\",\"R_Index:East\",\"R_Index:West\"]", "Review: All 4 Directions", 1 },
                    { 34, "{\"text\":\"fruit they grow hard if you are wet\",\"timeLimitSeconds\":90}", "Build speed across all directions", 19, 0.80m, 2, 8, "[]", "Speed Drill: All Directions", 2 },
                    { 35, "{\"gameType\":\"falling-characters\",\"timeLimitSeconds\":60,\"wordPool\":[\"r\",\"u\",\"e\",\"i\",\"w\",\"o\",\"t\",\"y\",\"g\",\"h\",\"c\",\"b\"]}", "Game challenge — hit all 4 directions", 20, 0.80m, 2, 10, "[]", "Play: Direction Challenge", 3 },
                    { 36, "{\"videoUrl\":\"/videos/cce.mp4\",\"durationSeconds\":90,\"completionThreshold\":0.9}", "Understand character-by-character entry and why speed matters", 1, 0.80m, 3, null, "[]", "CCE Typing Explained", 4 },
                    { 37, "{\"text\":\"as if of as of if as if of as if of as\",\"timeLimitSeconds\":90}", "Type short common words", 2, 0.80m, 3, 8, "[]", "Simple Words: as, if, of", 2 },
                    { 38, "{\"text\":\"all dad fall ask salad flask shall disk lid\",\"timeLimitSeconds\":90}", "Words using only home row push keys", 3, 0.80m, 3, 8, "[]", "Home Row Words", 2 },
                    { 39, "{\"text\":\"the and for are but not you all had her\",\"timeLimitSeconds\":90}", "Type the most common 3-letter words", 4, 0.80m, 3, 10, "[]", "Common 3-Letter Words", 2 },
                    { 40, "{\"sequence\":[\"the\",\"and\",\"for\",\"are\",\"you\",\"had\",\"her\",\"his\",\"its\",\"our\"]}", "Review short common words", 5, 0.80m, 3, null, "[]", "Review: Short Words", 1 },
                    { 41, "{\"text\":\"that with have this will your from they said each\",\"timeLimitSeconds\":90}", "Practice 4-letter common words", 6, 0.80m, 3, 10, "[]", "4-Letter Words", 2 },
                    { 42, "{\"text\":\"the quick red fox and the lazy dog sat still\",\"timeLimitSeconds\":60}", "Reach 10 WPM with common words", 7, 0.80m, 3, 10, "[]", "Speed Target: 10 WPM", 2 },
                    { 43, "{\"text\":\"in the and to a of it is was he for on are\",\"timeLimitSeconds\":90}", "Practice typing short phrases", 8, 0.80m, 3, 12, "[]", "Common Phrases", 2 },
                    { 44, "{\"text\":\"yes. no. wait; go. stop; run. sit. look; come.\",\"timeLimitSeconds\":90}", "Practice comma, period, and semicolons", 9, 0.80m, 3, 8, "[]", "Punctuation Basics", 2 },
                    { 45, "{\"text\":\"she was able to do the work and it felt right\",\"timeLimitSeconds\":60}", "Push your speed to 15 WPM", 10, 0.80m, 3, 15, "[]", "Speed Target: 15 WPM", 2 },
                    { 46, "{\"text\":\"the dog sat. she ran fast. he ate food. they slept.\",\"timeLimitSeconds\":90}", "Practice typing full short sentences", 11, 0.80m, 3, 12, "[]", "Common Sentences", 2 },
                    { 47, "{\"text\":\"to be or not to be that is the question here\",\"timeLimitSeconds\":60}", "Hit 20 WPM — a major CCE milestone", 12, 0.80m, 3, 20, "[]", "Speed Target: 20 WPM", 2 },
                    { 48, "{\"text\":\"work life time year day week made said great call\",\"timeLimitSeconds\":90}", "Random common words for consistency", 13, 0.80m, 3, 15, "[]", "Mixed Vocabulary Drill", 2 },
                    { 49, "{\"text\":\"she said the world is full of wonder and good things\",\"timeLimitSeconds\":120}", "Slow down and focus on zero errors", 14, 0.80m, 3, 10, "[]", "Accuracy Focus", 2 },
                    { 50, "{\"gameType\":\"word-race\",\"timeLimitSeconds\":60,\"wordPool\":[\"the\",\"and\",\"for\",\"you\",\"his\",\"her\",\"they\",\"have\",\"with\",\"this\",\"from\",\"that\",\"were\",\"said\",\"each\",\"when\",\"make\",\"like\",\"time\",\"just\"]}", "Race against the clock — how many words can you type?", 15, 0.80m, 3, 20, "[]", "Play: Word Race", 3 },
                    { 51, "{\"videoUrl\":\"/videos/chording-intro.mp4\",\"durationSeconds\":120,\"completionThreshold\":0.9}", "Learn how pressing multiple switches simultaneously types whole words", 1, 0.80m, 4, null, "[]", "What is Chording?", 4 },
                    { 52, "{\"steps\":[{\"instruction\":\"Press left ring + left middle + left index simultaneously to type 'the'\",\"targetInput\":\"L_Ring:Push+L_Middle:Push+L_Index:Push\",\"targetChar\":\"the\"}]}", "Press three switches at once to type 'the'", 2, 0.80m, 4, null, "[\"L_Ring:Push\",\"L_Middle:Push\",\"L_Index:Push\"]", "Your First Chord: the", 0 },
                    { 53, "{\"steps\":[{\"instruction\":\"Press left pinky + left ring + left middle simultaneously to type 'and'\",\"targetInput\":\"L_Pinky:Push+L_Ring:Push+L_Middle:Push\",\"targetChar\":\"and\"}]}", "Chord 'and' with left hand ring, middle, and pinky", 3, 0.80m, 4, null, "[\"L_Ring:Push\",\"L_Middle:Push\",\"L_Pinky:Push\"]", "Chord: and", 0 },
                    { 54, "{\"steps\":[{\"instruction\":\"Press left index push + left middle pull simultaneously to type 'for'\",\"targetInput\":\"L_Index:Push+L_Middle:Pull\",\"targetChar\":\"for\"}]}", "Chord 'for' with index and middle fingers", 4, 0.80m, 4, null, "[\"L_Index:Push\",\"L_Middle:Pull\"]", "Chord: for", 0 },
                    { 55, "{\"sequence\":[\"the\",\"and\",\"for\",\"the\",\"and\",\"for\",\"and\",\"the\",\"for\",\"the\",\"for\",\"and\"]}", "Practice your first three chords", 5, 0.80m, 4, null, "[]", "Review: the, and, for", 1 },
                    { 56, "{\"steps\":[{\"instruction\":\"Press right index pull + right middle pull simultaneously to type 'you'\",\"targetInput\":\"R_Index:Pull+R_Middle:Pull\",\"targetChar\":\"you\"}]}", "Chord 'you' with right hand", 6, 0.80m, 4, null, "[\"R_Index:Pull\",\"R_Middle:Pull\"]", "Chord: you", 0 },
                    { 57, "{\"steps\":[{\"instruction\":\"Press right ring pull + right index push simultaneously to type 'with'\",\"targetInput\":\"R_Ring:Pull+R_Index:Push\",\"targetChar\":\"with\"}]}", "Chord 'with' with right hand ring and index", 7, 0.80m, 4, null, "[\"R_Ring:Pull\",\"R_Index:Push\"]", "Chord: with", 0 },
                    { 58, "{\"steps\":[{\"instruction\":\"Press left index east + left index push simultaneously to type 'that'\",\"targetInput\":\"L_Index:East+L_Index:Push\",\"targetChar\":\"that\"}]}", "Chord 'that' with left hand", 8, 0.80m, 4, null, "[\"L_Index:East\",\"L_Index:Push\"]", "Chord: that", 0 },
                    { 59, "{\"text\":\"the and for you with that the with you and for that\",\"timeLimitSeconds\":90}", "Build speed with your first 6 chords", 9, 0.80m, 4, 15, "[]", "Practice: First 6 Chords", 2 },
                    { 60, "{\"steps\":[{\"instruction\":\"Press left pinky push + left middle pull simultaneously to type 'have'\",\"targetInput\":\"L_Pinky:Push+L_Middle:Pull\",\"targetChar\":\"have\"}]}", "Chord 'have' with left pinky and middle", 10, 0.80m, 4, null, "[\"L_Pinky:Push\",\"L_Middle:Pull\"]", "Chord: have", 0 },
                    { 61, "{\"steps\":[{\"instruction\":\"Press right ring push + right middle pull simultaneously to type 'this'\",\"targetInput\":\"R_Ring:Push+R_Middle:Pull\",\"targetChar\":\"this\"}]}", "Chord 'this' with right ring and middle", 11, 0.80m, 4, null, "[\"R_Ring:Push\",\"R_Middle:Pull\"]", "Chord: this", 0 },
                    { 62, "{\"steps\":[{\"instruction\":\"Press left index pull + left ring push simultaneously to type 'from'\",\"targetInput\":\"L_Index:Pull+L_Ring:Push\",\"targetChar\":\"from\"}]}", "Chord 'from' with left fingers", 12, 0.80m, 4, null, "[\"L_Index:Pull\",\"L_Ring:Push\"]", "Chord: from", 0 },
                    { 63, "{\"sequence\":[\"the\",\"and\",\"for\",\"you\",\"with\",\"that\",\"have\",\"this\",\"from\",\"the\",\"with\",\"you\",\"and\",\"that\",\"this\",\"have\",\"from\",\"for\"]}", "Review all chords learned so far", 13, 0.80m, 4, null, "[]", "Review: All 9 Chords", 1 },
                    { 64, "{\"text\":\"the cat and you have this from that with the dog\",\"timeLimitSeconds\":60}", "Push chord speed to 20 WPM", 14, 0.80m, 4, 20, "[]", "Speed Challenge: Basic Chords", 2 },
                    { 65, "{\"gameType\":\"word-race\",\"timeLimitSeconds\":60,\"wordPool\":[\"the\",\"and\",\"for\",\"you\",\"with\",\"that\",\"have\",\"this\",\"from\",\"they\",\"were\",\"said\"]}", "Game challenge — how fast can you chord?", 15, 0.80m, 4, 25, "[]", "Play: Chord Race", 3 },
                    { 66, "{\"videoUrl\":\"/videos/advanced-chording.mp4\",\"durationSeconds\":120,\"completionThreshold\":0.9}", "Rolling chords, timing, and building a chord vocabulary", 1, 0.80m, 5, null, "[]", "Advanced Chording Techniques", 4 },
                    { 67, "{\"steps\":[{\"instruction\":\"Press right index west + right ring pull simultaneously to type 'would'\",\"targetInput\":\"R_Index:West+R_Ring:Pull\",\"targetChar\":\"would\"}]}", "Chord 'would' with right hand", 2, 0.80m, 5, null, "[\"R_Index:West\",\"R_Ring:Pull\"]", "Chord: would", 0 },
                    { 68, "{\"steps\":[{\"instruction\":\"Press right ring push + right index west simultaneously to type 'should'\",\"targetInput\":\"R_Ring:Push+R_Index:West\",\"targetChar\":\"should\"}]}", "Chord 'should' with right hand", 3, 0.80m, 5, null, "[\"R_Ring:Push\",\"R_Index:West\"]", "Chord: should", 0 },
                    { 69, "{\"steps\":[{\"instruction\":\"Press left middle pull + left ring pull simultaneously to type 'could'\",\"targetInput\":\"L_Middle:Pull+L_Ring:Pull\",\"targetChar\":\"could\"}]}", "Chord 'could' with left hand", 4, 0.80m, 5, null, "[\"L_Middle:Pull\",\"L_Ring:Pull\"]", "Chord: could", 0 },
                    { 70, "{\"sequence\":[\"would\",\"should\",\"could\",\"would\",\"could\",\"should\",\"should\",\"would\",\"could\",\"would\",\"should\",\"could\"]}", "Practice would, should, could", 5, 0.80m, 5, null, "[]", "Review: Modal Verbs", 1 },
                    { 71, "{\"steps\":[{\"instruction\":\"Press right index push + right middle push + right ring pull simultaneously to type 'there'\",\"targetInput\":\"R_Index:Push+R_Middle:Push+R_Ring:Pull\",\"targetChar\":\"there\"}]}", "Chord 'there' with right fingers", 6, 0.80m, 5, null, "[\"R_Index:Push\",\"R_Middle:Push\",\"R_Ring:Pull\"]", "Chord: there", 0 },
                    { 72, "{\"steps\":[{\"instruction\":\"Press right index pull + right middle push + right ring pull simultaneously to type 'their'\",\"targetInput\":\"R_Index:Pull+R_Middle:Push+R_Ring:Pull\",\"targetChar\":\"their\"}]}", "Chord 'their' — similar to 'there', different fingers", 7, 0.80m, 5, null, "[\"R_Index:Pull\",\"R_Middle:Push\",\"R_Ring:Pull\"]", "Chord: their", 0 },
                    { 73, "{\"steps\":[{\"instruction\":\"Press right index push + right ring pull simultaneously to type 'they'\",\"targetInput\":\"R_Index:Push+R_Ring:Pull\",\"targetChar\":\"they\"}]}", "Chord 'they' with right hand", 8, 0.80m, 5, null, "[\"R_Index:Push\",\"R_Ring:Pull\"]", "Chord: they", 0 },
                    { 74, "{\"text\":\"they would have the would you should be there their\",\"timeLimitSeconds\":90}", "Mix old and new chords for fluency", 9, 0.80m, 5, 25, "[]", "Practice: Mixed Chords", 2 },
                    { 75, "{\"steps\":[{\"instruction\":\"Press left pinky push + right index pull + right middle pull simultaneously to type 'people'\",\"targetInput\":\"L_Pinky:Push+R_Index:Pull+R_Middle:Pull\",\"targetChar\":\"people\"}]}", "Chord 'people' with both hands", 10, 0.80m, 5, null, "[\"L_Pinky:Push\",\"R_Index:Pull\",\"R_Middle:Pull\"]", "Chord: people", 0 },
                    { 76, "{\"steps\":[{\"instruction\":\"Press right index west + right middle pull simultaneously to type 'which'\",\"targetInput\":\"R_Index:West+R_Middle:Pull\",\"targetChar\":\"which\"}]}", "Chord 'which' with right hand", 11, 0.80m, 5, null, "[\"R_Index:West\",\"R_Middle:Pull\"]", "Chord: which", 0 },
                    { 77, "{\"steps\":[{\"instruction\":\"Press left pinky pull + left middle push simultaneously to type 'about'\",\"targetInput\":\"L_Pinky:Pull+L_Middle:Push\",\"targetChar\":\"about\"}]}", "Chord 'about' with left hand", 12, 0.80m, 5, null, "[\"L_Pinky:Pull\",\"L_Middle:Push\"]", "Chord: about", 0 },
                    { 78, "{\"text\":\"people which about would should could there their they\",\"timeLimitSeconds\":90}", "Speed drill with all advanced chords", 13, 0.80m, 5, 30, "[]", "Practice: All Advanced Chords", 2 },
                    { 79, "{\"text\":\"the people would have said they should be there with you about which could be for all\",\"timeLimitSeconds\":60}", "Push for 40 WPM — elite CharaChorder speed", 14, 0.80m, 5, 40, "[]", "Speed Target: 40 WPM", 2 },
                    { 80, "{\"gameType\":\"word-race\",\"timeLimitSeconds\":60,\"wordPool\":[\"the\",\"and\",\"for\",\"you\",\"with\",\"that\",\"have\",\"this\",\"from\",\"they\",\"would\",\"should\",\"could\",\"there\",\"their\",\"people\",\"which\",\"about\"]}", "Final game challenge — show off everything you have learned", 15, 0.80m, 5, 40, "[]", "Play: Advanced Challenge", 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 80);
        }
    }
}
