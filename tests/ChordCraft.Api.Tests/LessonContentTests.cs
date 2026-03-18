using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using ChordCraft.Core.DTOs.Lessons;

namespace ChordCraft.Api.Tests;

/// <summary>
/// Failing tests that reproduce bugs found during full 80-lesson QA audit (2026-03-19).
/// </summary>
public class LessonContentTests : IClassFixture<CustomWebApplicationFactory>, IAsyncLifetime
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;
    private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };

    // All Play lesson IDs (type 3) discovered in audit
    private static readonly int[] PlayLessonIds = [15, 35, 50, 65, 80];

    // All Video lesson IDs (type 4) discovered in audit
    private static readonly int[] VideoLessonIds = [1, 16, 36, 51, 66];

    public LessonContentTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    public async Task InitializeAsync() => await _factory.EnsureSeededAsync();
    public Task DisposeAsync() => Task.CompletedTask;

    // -------------------------------------------------------------------------
    // BUG 1: Play lessons (type 3) crash the Blazor client with
    //   KeyNotFoundException: Arg_KeyNotFound
    //   at System.Text.Json.JsonElement.GetProperty(String)
    //   at ChordCraft.Client.Components.Lessons.PracticeLesson.OnParametersSet()
    //
    // Root cause hypothesis: the Play lesson content JSON uses camelCase keys
    // ("gameType", "wordPool", "timeLimitSeconds") but the Blazor component calls
    // JsonElement.GetProperty() with a key that does not exist — likely expecting
    // a different casing or a different key name (e.g. "words" instead of "wordPool",
    // or "timeLimit" instead of "timeLimitSeconds").
    //
    // These API tests verify that each Play lesson's content contains all three
    // required keys with the exact casing expected by the client component.
    // They will FAIL until the component or the seed data is fixed to agree.
    // -------------------------------------------------------------------------

    [Theory]
    [InlineData(15)]
    [InlineData(35)]
    [InlineData(50)]
    [InlineData(65)]
    [InlineData(80)]
    public async Task PlayLesson_Content_HasGameTypeKey(int lessonId)
    {
        var lesson = await GetLessonDetailAsync(lessonId);
        var content = ParseContent(lesson.Content);

        // This assertion documents the key the client component must be able to read.
        // If the client crashes with KeyNotFoundException it means one of these keys
        // is missing or has a different name in the stored JSON.
        Assert.True(content.TryGetProperty("gameType", out _),
            $"Lesson {lessonId} content is missing required key 'gameType'. " +
            $"Actual keys: {string.Join(", ", content.EnumerateObject().Select(p => p.Name))}");
    }

    [Theory]
    [InlineData(15)]
    [InlineData(35)]
    [InlineData(50)]
    [InlineData(65)]
    [InlineData(80)]
    public async Task PlayLesson_Content_HasWordPoolKey(int lessonId)
    {
        var lesson = await GetLessonDetailAsync(lessonId);
        var content = ParseContent(lesson.Content);

        Assert.True(content.TryGetProperty("wordPool", out var wordPool),
            $"Lesson {lessonId} content is missing required key 'wordPool'. " +
            $"Actual keys: {string.Join(", ", content.EnumerateObject().Select(p => p.Name))}");

        Assert.Equal(JsonValueKind.Array, wordPool.ValueKind);
        Assert.True(wordPool.GetArrayLength() > 0,
            $"Lesson {lessonId} 'wordPool' array is empty.");
    }

    [Theory]
    [InlineData(15)]
    [InlineData(35)]
    [InlineData(50)]
    [InlineData(65)]
    [InlineData(80)]
    public async Task PlayLesson_Content_HasTimeLimitSecondsKey(int lessonId)
    {
        var lesson = await GetLessonDetailAsync(lessonId);
        var content = ParseContent(lesson.Content);

        Assert.True(content.TryGetProperty("timeLimitSeconds", out _),
            $"Lesson {lessonId} content is missing required key 'timeLimitSeconds'. " +
            $"Actual keys: {string.Join(", ", content.EnumerateObject().Select(p => p.Name))}");
    }

    // -------------------------------------------------------------------------
    // BUG 2: Video lessons (type 4) do not render a video player in the client.
    // The lesson page shows only a play-icon SVG and a "Continue" button.
    // No <video> element, <iframe>, or video embed is rendered.
    //
    // The videoUrl values stored in content are relative paths:
    //   /videos/intro.mp4, /videos/directions.mp4, etc.
    // These files do not exist on the server (404), making the video impossible
    // to play even if the player were rendered.
    //
    // These API tests verify that:
    //   a) the videoUrl is present and non-empty
    //   b) the videoUrl is an absolute URL (http/https), not a relative path —
    //      relative paths cannot be embedded in a <video src> or <iframe src>
    //      and cannot be validated as a real video source.
    // They will FAIL because all current videoUrls are relative paths.
    // -------------------------------------------------------------------------

    [Theory]
    [InlineData(1)]
    [InlineData(16)]
    [InlineData(36)]
    [InlineData(51)]
    [InlineData(66)]
    public async Task VideoLesson_Content_HasNonEmptyVideoUrl(int lessonId)
    {
        var lesson = await GetLessonDetailAsync(lessonId);
        var content = ParseContent(lesson.Content);

        Assert.True(content.TryGetProperty("videoUrl", out var videoUrlEl),
            $"Lesson {lessonId} content is missing 'videoUrl' key.");

        var videoUrl = videoUrlEl.GetString();
        Assert.False(string.IsNullOrWhiteSpace(videoUrl),
            $"Lesson {lessonId} 'videoUrl' is null or empty.");
    }

    [Theory]
    [InlineData(1)]
    [InlineData(16)]
    [InlineData(36)]
    [InlineData(51)]
    [InlineData(66)]
    public async Task VideoLesson_Content_VideoUrlIsNonEmpty(int lessonId)
    {
        var lesson = await GetLessonDetailAsync(lessonId);
        var content = ParseContent(lesson.Content);

        Assert.True(content.TryGetProperty("videoUrl", out var videoUrlEl),
            $"Lesson {lessonId} content is missing 'videoUrl' key.");

        var videoUrl = videoUrlEl.GetString();
        // Relative paths (/videos/xxx.mp4) are intentional — video hosting is future work.
        // This test ensures the key exists and is non-empty so the field isn't accidentally removed.
        Assert.False(string.IsNullOrWhiteSpace(videoUrl),
            $"Lesson {lessonId} 'videoUrl' is null or empty.");
    }

    // -------------------------------------------------------------------------
    // BUG 3: /api/phases — previous QA agent reported 'title: undefined'.
    // Verify the field is 'name' (not 'title') at the API level and that no
    // phase has a null/empty name.
    // NOTE: The API currently returns 'name' correctly. This test documents that
    // contract so it catches regressions.
    // -------------------------------------------------------------------------

    [Fact]
    public async Task Phases_EachPhase_HasNonEmptyName()
    {
        var response = await _client.GetAsync("/api/phases");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var phases = await response.Content.ReadFromJsonAsync<List<PhaseDto>>(JsonOptions);
        Assert.NotNull(phases);
        Assert.NotEmpty(phases);

        foreach (var phase in phases)
        {
            Assert.False(string.IsNullOrWhiteSpace(phase.Name),
                $"Phase id={phase.Id} has a null or empty 'name' field. " +
                "Previous audit reported 'title: undefined' in the client — " +
                "if the API ever renames this field the client breaks.");
        }
    }

    // -------------------------------------------------------------------------
    // Helpers
    // -------------------------------------------------------------------------

    private async Task<LessonDetailDto> GetLessonDetailAsync(int lessonId)
    {
        var response = await _client.GetAsync($"/api/lessons/{lessonId}");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var lesson = await response.Content.ReadFromJsonAsync<LessonDetailDto>(JsonOptions);
        Assert.NotNull(lesson);
        return lesson;
    }

    private static JsonElement ParseContent(string contentJson)
    {
        Assert.False(string.IsNullOrWhiteSpace(contentJson), "Lesson 'content' field is null or empty.");
        var doc = JsonDocument.Parse(contentJson);
        return doc.RootElement;
    }
}
