using System.Security.Claims;
using ChordCraft.Core.DTOs.Progress;
using ChordCraft.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChordCraft.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProgressController : ControllerBase
{
    private readonly IProgressService _progressService;
    private readonly IStatsService _statsService;

    public ProgressController(IProgressService progressService, IStatsService statsService)
    {
        _progressService = progressService;
        _statsService = statsService;
    }

    [HttpPost("~/api/attempts")]
    public async Task<IActionResult> SubmitAttempt(SubmitAttemptRequest request)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        return Ok(await _progressService.SubmitAttemptAsync(userId, request));
    }

    [HttpGet]
    public async Task<IActionResult> GetProgress()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        return Ok(await _progressService.GetAllProgressAsync(userId));
    }

    [HttpGet("~/api/stats")]
    public async Task<IActionResult> GetStats()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        return Ok(await _statsService.GetStatsAsync(userId));
    }
}
