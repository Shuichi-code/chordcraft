using System.Security.Claims;
using ChordCraft.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChordCraft.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BadgesController : ControllerBase
{
    private readonly IBadgeService _badgeService;
    public BadgesController(IBadgeService badgeService) => _badgeService = badgeService;

    [HttpGet]
    public async Task<IActionResult> GetBadges()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        return Ok(await _badgeService.GetBadgesForUserAsync(userId));
    }
}
