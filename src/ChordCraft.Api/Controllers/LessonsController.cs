using System.Security.Claims;
using ChordCraft.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChordCraft.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LessonsController : ControllerBase
{
    private readonly ILessonService _lessonService;
    private readonly IProgressService _progressService;

    public LessonsController(ILessonService lessonService, IProgressService progressService)
    {
        _lessonService = lessonService;
        _progressService = progressService;
    }

    [HttpGet("~/api/phases")]
    public async Task<IActionResult> GetPhases() => Ok(await _lessonService.GetAllPhasesAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetLesson(int id)
    {
        var lesson = await _lessonService.GetLessonAsync(id);
        return lesson is null ? NotFound() : Ok(lesson);
    }

    [Authorize]
    [HttpGet("{id}/progress")]
    public async Task<IActionResult> GetLessonProgress(int id)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var progress = (await _progressService.GetAllProgressAsync(userId)).FirstOrDefault(p => p.LessonId == id);
        return progress is null ? NotFound() : Ok(progress);
    }
}
