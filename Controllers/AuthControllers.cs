using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loguinRequest)
    {
        var users = await _userService.Get();
        var user = users.FirstOrDefault(u => u.Email == loguinRequest.Email && u.Password == loguinRequest.Password);
        if (user == null)
        {
            return Unauthorized(new { message = "Invalid username or password." });
        }
        return Ok(new
        {
            message = "Login successful",
            user.UserId,
            user.Name,
            user.LastName,
            user.Email,
            user.Phone
        });
    }
}