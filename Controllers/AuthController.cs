﻿using ProjectBlu.Services.Interfaces;

namespace ProjectBlu.Controllers;

[Route("api/[controller]")]
[Produces("application/json")]
[Authorize]
[ApiController]
public class AuthController : ApiController
{
    private readonly IAuthService _authService;
    private readonly IOIDCService _oidcService;

    private const string OIDC_STATE = "oidc-state";
    private const string OIDC_NONCE = "oidc-nonce";

    public AuthController(IAuthService authService, IOIDCService oidcService)
    {
        _authService = authService;
        _oidcService = oidcService;
    }

    [HttpPost("login"), AllowAnonymous]
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

    [HttpGet("oidc/{provider}"), AllowAnonymous]
    public IActionResult LoginWithOIDC([FromRoute] string provider)
    {
        var response = _oidcService.CreateAuthorizationUrl(provider);

        if (response == null)
        {
            return BadRequest($"Invalid provider: {provider}");
        }

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddMinutes(10)
        };

        Response.Cookies.Append(OIDC_NONCE, response.Nonce, cookieOptions);
        Response.Cookies.Append(OIDC_STATE, response.State, cookieOptions);

        return Redirect(response.Redirect);
    }

    [HttpPost("oidc/{provider}"), AllowAnonymous]
    public IActionResult OnOIDCLogin(
        [FromBody] OpenIdLoginRequest request,
        [FromRoute] string provider
    )
    {
        if (!_oidcService.IsValidProvider(provider))
        {
            return BadRequest($"Invalid provider: {provider}");
        }

        Request.Cookies.TryGetValue(OIDC_STATE, out string state);
        Request.Cookies.TryGetValue(OIDC_NONCE, out string nonce);

        if (state == null || nonce == null || request.State == null || state != request.State)
        {
            return BadRequest("Missing valid state or nonce");
        }

        Response.Cookies.Delete(OIDC_STATE);
        Response.Cookies.Delete(OIDC_NONCE);

        return Ok();
    }
}