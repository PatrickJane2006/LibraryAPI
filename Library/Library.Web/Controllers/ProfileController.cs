using Library.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Library.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] 
public class ProfileController : ControllerBase
{
    private readonly IUserService _userService;

    public ProfileController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetProfile()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null) return Unauthorized("Нет ID");

        if (!int.TryParse(userIdClaim.Value, out int userId))
            return Unauthorized("Некорректный ID");

        var profile = await _userService.GetCurrentUserAsync(userId);
        return Ok(profile);
    }
}
