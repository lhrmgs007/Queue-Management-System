using Microsoft.AspNetCore.Mvc;
using QMS.Core.DTOs;
using QMS.Services.Authentication;
using QMS.Services.Users;

namespace QMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IAuthenticationService _authService;

    public AuthController(IUserService userService, IAuthenticationService authService)
    {
        _userService = userService;
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _userService.AuthenticateAsync(request.Username, request.Password);
        if (user == null)
            return Unauthorized(new { message = "Invalid username or password" });

        var token = _authService.GenerateToken(user);
        return Ok(new LoginResponse
        {
            UserId = user.UserId,
            Username = user.Username,
            RoleName = user.Role?.RoleName ?? "Unknown",
            Token = token,
            ExpiresAt = DateTime.UtcNow.AddMinutes(1440)
        });
    }
}
