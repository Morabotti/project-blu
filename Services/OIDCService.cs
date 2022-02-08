using ProjectBlu.Enums;
using ProjectBlu.Models;
using ProjectBlu.Services.Interfaces;
using ProjectBlu.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

namespace ProjectBlu.Services;

public class OIDCService : IOIDCService
{
    private readonly OIDCSettings _settings;
    private readonly IMapper _mapper;
    private readonly ILogger<IOIDCService> _logger;

    private readonly string _domain;

    private bool _providerMicrosoft;
    private bool _providerGoogle;

    private const string OPENID_SCOPE = "openid profile email";

    public OIDCService(
        IConfiguration configuration,
        ILogger<IOIDCService> logger,
        IMapper mapper
    )
    {
        _mapper = mapper;
        _settings = configuration.GetSection("OIDC").Get<OIDCSettings>();
        _domain = configuration.GetValue<string>("Domain");
        _logger = logger;

        _ = PopulateGoogleAsync();
        _ = PopulateMicrosoftAsync();
    }

    public Task<User> LoginAsync(string provider, string code, string nonce)
    {
        return provider switch
        {
            "google" => LoginWithGoogleAsync(code, nonce),
            "microsoft" => LoginWithMicrosoftAsync(code, nonce),
            _ => null
        };
    }

    public AuthorizationResponse CreateAuthorizationUrl(string provider)
    {
        if (!IsValidProvider(provider))
        {
            return null;
        }

        var response = new AuthorizationResponse
        {
            State = $"{Guid.NewGuid()}-{Guid.NewGuid()}",
            Nonce = Guid.NewGuid().ToString("N")
        };

        switch (provider)
        {
            case "google":
                response.Redirect = $"{_settings.Google.EndpointAuthorization}?client_id={_settings.Google.ClientId}" +
                    $"&response_type=code&scope={OPENID_SCOPE}&prompt=select_account" +
                    $"&redirect_uri={_domain}/auth/oidc/cb/google/" +
                    $"&state={response.State}&nonce={response.Nonce}";
                 break;

            case "microsoft":
                response.Redirect = $"{_settings.Microsoft.EndpointAuthorization}?client_id={_settings.Microsoft.ClientId}" +
                    $"&response_type=code&scope={OPENID_SCOPE}&response_mode=fragment&prompt=select_account" +
                    $"&redirect_uri={_domain}/auth/oidc/cb/microsoft/" +
                    $"&state={response.State}&nonce={response.Nonce}";
                break;
        }

        return response;
    }

    public bool IsValidProvider(string provider)
    {
        return provider switch
        {
            "google" => _providerGoogle,
            "microsoft" => _providerMicrosoft,
            _ => false
        };
    }

    public Dictionary<string, bool> GetProviders()
    {
        return new Dictionary<string, bool>
        {
            { "google", _providerGoogle },
            { "microsoft", _providerMicrosoft }
        };
    }

    private async Task<User> LoginWithGoogleAsync(string code, string nonce)
    {
        var opt = _settings.Google;
        var redirect = $"{_domain}/auth/oidc/cb/google/";

        var response = await ValidateCodeAsync(opt.ClientId, opt.ClientSecret, redirect, code, opt.EndpointToken);
        var tokenResponse = await ParseBodyAsync(response);
        var jwtToken = ValidateToken(tokenResponse, opt.Issuer, nonce);

        var user = GenerateUser(jwtToken, AuthProvider.Google);
        return user;
    }

    private async Task<User> LoginWithMicrosoftAsync(string code, string nonce)
    {
        var opt = _settings.Microsoft;
        var redirect = $"{_domain}/auth/oidc/cb/microsoft/";

        var response = await ValidateCodeAsync(opt.ClientId, opt.ClientSecret, redirect, code, opt.EndpointToken);
        var tokenResponse = await ParseBodyAsync(response);
        ValidateToken(tokenResponse, opt.Issuer, nonce);
        var accessToken = ReadJwtToken(tokenResponse.AccessToken);

        var user = GenerateUser(accessToken, AuthProvider.Microsoft);
        return user;
    }

    private static JwtSecurityToken ValidateToken(OIDCTokenResponse token, string issuer, string nonce)
    {
        JwtSecurityToken jwtToken = ReadJwtToken(token.IdToken);

        if (jwtToken.Issuer != issuer)
        {
            throw new UnauthorizedAccessException("Invalid issuer.");
        }

        var tokenNonce = jwtToken.Claims.First(claim => claim.Type == "nonce");

        if (tokenNonce == null || tokenNonce.Value != nonce)
        {
            throw new UnauthorizedAccessException("Invalid nonce.");
        }

        return jwtToken;
    }

    private static User GenerateUser(JwtSecurityToken token, AuthProvider provider)
    {
        var email = token.Claims.First(claim => claim.Type == JwtClaims.Email).Value;
        var firstName = token.Claims.First(claim => claim.Type == JwtClaims.GivenName).Value;
        var lastName = token.Claims.First(claim => claim.Type == JwtClaims.FamilyName).Value;

        var user = new User
        {
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            Password = null,
            Provider = provider,
        };

        if (provider == AuthProvider.Google)
        {
            var picture = token.Claims.FirstOrDefault(claim => claim.Type == JwtClaims.Picture).Value;

            if (picture == null)
            {
                return user;
            }

            user.Image = new ImageAsset
            {
                Source = picture
            };
        }

        return user;
    }

    private static JwtSecurityToken ReadJwtToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        return handler.ReadJwtToken(token);
    }

    private static async Task<OIDCTokenResponse> ParseBodyAsync(HttpResponseMessage response)
    {
        string body = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<OIDCTokenResponse>(body);
    }

    private static async Task<HttpResponseMessage> ValidateCodeAsync(
        string clientId,
        string secret,
        string redirect,
        string code,
        string endpoint
    )
    {
        var body = new Dictionary<string, string>
        {
            { "grant_type", "authorization_code" },
            { "client_id", clientId },
            { "client_secret", secret },
            { "redirect_uri", redirect },
            { "code" , code }
        };

        var client = new HttpClient();
        var content = new FormUrlEncodedContent(body);

        var response = await client.PostAsync(endpoint, content);

        if (!response.IsSuccessStatusCode)
        {
            throw new UnauthorizedAccessException("Invalid code.");
        }

        return response;
    }

    private async Task PopulateGoogleAsync()
    {
        if (_settings.Google == null || !_settings.Google.HasInitialValues())
        {
            _providerGoogle = false;
            return;
        }

        var discovery = await GetDiscoveryOptionsAsync(_settings.Google.DiscoveryUrl);

        if (discovery == null)
        {
            _logger.LogWarning("OIDC configs for Google were found, but discovery has failed.");
            _providerGoogle = false;
            return;
        }

        _settings.Google.Issuer = discovery.Issuer;
        _settings.Google.EndpointAuthorization = discovery.AuthorizationEndpoint;
        _settings.Google.EndpointToken = discovery.TokenEndpoint;

        _logger.LogInformation("OIDC Configured for Google");
        _providerGoogle = true;
    }

    private async Task PopulateMicrosoftAsync()
    {
        if (_settings.Microsoft == null || !_settings.Microsoft.HasInitialValues())
        {
            _providerMicrosoft = false;
            return;
        }

        var discovery = await GetDiscoveryOptionsAsync(_settings.Microsoft.DiscoveryUrl);

        if (discovery == null)
        {
            _logger.LogWarning("OIDC configs for Microsoft were found, but discovery has failed.");
            _providerMicrosoft = false;
            return;
        }

        var issuer = discovery.Issuer.Replace("{tenantid}", _settings.Microsoft.Tenant);

        _settings.Microsoft.Issuer = issuer;
        _settings.Microsoft.EndpointAuthorization = discovery.AuthorizationEndpoint;
        _settings.Microsoft.EndpointToken = discovery.TokenEndpoint;

        _logger.LogInformation("OIDC Configured for Microsoft");
        _providerMicrosoft = true;
    }

    private static async Task<DiscoveryResponse?> GetDiscoveryOptionsAsync(string url)
    {
        var client = new HttpClient();

        try
        {
            HttpResponseMessage response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<DiscoveryResponse>(json);
        }
        catch (Exception)
        {
            return null;
        }
    }
}
