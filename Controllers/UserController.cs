using ProjectBlu.Services.Interfaces;

namespace ProjectBlu.Controllers;

[Route("api/[controller]")]
[Produces("application/json")]
[Authorize]
[ApiController]
public class UserController : ApiController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("setup"), AllowAnonymous]
    public async Task<IActionResult> CreateSetupUser([FromBody] CreateUserRequest request)
    {
        var response = await _userService.CreateFirstUserAsync(request);
        return HttpResponse(response);
    }

    [HttpGet("setup"), AllowAnonymous]
    public async Task<IActionResult> CanSetup()
    {
        var canSetup = await _userService.CanSetupAsync();
        return HttpResponse(new Response<bool>(canSetup));
    }
}