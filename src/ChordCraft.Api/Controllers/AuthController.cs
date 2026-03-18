using System.Security.Claims;
using ChordCraft.Core.DTOs.Auth;
using ChordCraft.Core.Entities;
using ChordCraft.Core.Interfaces;
using ChordCraft.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChordCraft.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly AppDbContext _db;

    public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService, AppDbContext db)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _db = db;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponse>> Register(RegisterRequest request)
    {
        var user = new User { UserName = request.Email, Email = request.Email, DisplayName = request.DisplayName, CreatedAt = DateTime.UtcNow, LastActiveAt = DateTime.UtcNow };
        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded) return BadRequest(result.Errors);
        var token = _tokenService.GenerateAccessToken(user);
        return Ok(new AuthResponse(token, _tokenService.GenerateRefreshToken(), DateTime.UtcNow.AddHours(1), user.DisplayName ?? ""));
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null) return Unauthorized("Invalid credentials");
        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (!result.Succeeded) return Unauthorized("Invalid credentials");
        user.LastActiveAt = DateTime.UtcNow;
        await _userManager.UpdateAsync(user);
        var token = _tokenService.GenerateAccessToken(user);
        return Ok(new AuthResponse(token, _tokenService.GenerateRefreshToken(), DateTime.UtcNow.AddHours(1), user.DisplayName ?? ""));
    }

    [Authorize]
    [HttpPost("migrate")]
    public async Task<IActionResult> Migrate(MigrateProgressRequest request)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        foreach (var p in request.Progress)
            _db.UserProgress.Add(new UserProgress { UserId = userId, LessonId = p.LessonId, BestStars = p.BestStars, BestAccuracy = p.BestAccuracy, BestSpeed = p.BestSpeed, TotalAttempts = p.TotalAttempts, FirstCompletedAt = DateTime.UtcNow, LastAttemptAt = DateTime.UtcNow });
        foreach (var a in request.Attempts)
            _db.LessonAttempts.Add(new LessonAttempt { Id = Guid.NewGuid(), UserId = userId, LessonId = a.LessonId, StartedAt = a.StartedAt, CompletedAt = a.CompletedAt, Accuracy = a.Accuracy, Speed = a.Speed, Stars = a.Stars, Points = a.Points, Passed = a.Passed });
        await _db.SaveChangesAsync();
        return Ok();
    }
}
