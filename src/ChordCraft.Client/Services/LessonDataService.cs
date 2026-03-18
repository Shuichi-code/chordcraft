using System.Net.Http.Json;
using ChordCraft.Core.DTOs.Lessons;

namespace ChordCraft.Client.Services;

public class LessonDataService
{
    private readonly HttpClient _http;
    private List<PhaseDto>? _cachedPhases;

    public LessonDataService(HttpClient http) => _http = http;

    public async Task<List<PhaseDto>> GetPhasesAsync()
    {
        _cachedPhases ??= await _http.GetFromJsonAsync<List<PhaseDto>>("api/phases") ?? [];
        return _cachedPhases;
    }

    public async Task<LessonDetailDto?> GetLessonAsync(int id)
    {
        var response = await _http.GetAsync($"api/lessons/{id}");
        if (!response.IsSuccessStatusCode) return null;
        return await response.Content.ReadFromJsonAsync<LessonDetailDto>();
    }
}
