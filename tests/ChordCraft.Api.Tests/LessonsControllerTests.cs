using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using ChordCraft.Core.DTOs.Lessons;

namespace ChordCraft.Api.Tests;

public class LessonsControllerTests : IClassFixture<CustomWebApplicationFactory>, IAsyncLifetime
{
    private readonly CustomWebApplicationFactory _factory;
    private readonly HttpClient _client;
    private static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };

    public LessonsControllerTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    public async Task InitializeAsync() => await _factory.EnsureSeededAsync();
    public Task DisposeAsync() => Task.CompletedTask;

    [Fact]
    public async Task GetPhases_ReturnsAllPhasesWithLessons()
    {
        var response = await _client.GetAsync("/api/phases");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var phases = await response.Content.ReadFromJsonAsync<List<PhaseDto>>(JsonOptions);
        Assert.NotNull(phases);
        Assert.True(phases.Count >= 2);

        // The real seed data: Phase 1 is "Switch Basics"
        var firstPhase = phases.OrderBy(p => p.Order).First();
        Assert.Equal("Switch Basics", firstPhase.Name);
        Assert.NotEmpty(firstPhase.Lessons);
    }

    [Fact]
    public async Task GetLesson_WithValidId_ReturnsLessonDetail()
    {
        var response = await _client.GetAsync("/api/lessons/1");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var lesson = await response.Content.ReadFromJsonAsync<LessonDetailDto>(JsonOptions);
        Assert.NotNull(lesson);
        Assert.Equal(1, lesson.Id);
        // Lesson 1 from seed is "Introduction to CharaChorder" (Video)
        Assert.Equal("Introduction to CharaChorder", lesson.Title);
        Assert.False(string.IsNullOrEmpty(lesson.Content));
    }

    [Fact]
    public async Task GetLesson_WithInvalidId_Returns404()
    {
        var response = await _client.GetAsync("/api/lessons/999");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}
