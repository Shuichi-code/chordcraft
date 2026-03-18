using ChordCraft.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChordCraft.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChordsController : ControllerBase
{
    private readonly IChordService _chordService;
    public ChordsController(IChordService chordService) => _chordService = chordService;

    [HttpGet]
    public async Task<IActionResult> GetChords([FromQuery] int? difficulty, [FromQuery] string? category)
        => Ok(await _chordService.GetChordsAsync(difficulty, category));
}
