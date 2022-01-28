using ProjectBlu.Dto.Authentication;
using ProjectBlu.Services.Interfaces;

namespace ProjectBlu.Controllers;

[Route("api/[controller]")]
[Produces("application/json")]
[Authorize]
[ApiController]
public class AuthController : ApiController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var response = await _authService.LoginAsync(request);
        return HttpResponse(response);
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMe()
    {
        var user = GetUserClaim();
        var response = await _authService.GetUserAsync(user.Id);

        return HttpResponse(response);
    }
}