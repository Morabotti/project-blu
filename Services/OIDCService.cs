using ProjectBlu.Services.Interfaces;
using ProjectBlu.Settings;
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
                    $"&response_type=code&scope=${OPENID_SCOPE}&prompt=select_account" +
                    $"&redirect_uri={_domain}/auth/oidc/cb/google/" +
                    $"&state={response.State}&nonce={response.Nonce}";
                 break;
            case "microsoft":
                response.Redirect = $"{_settings.Microsoft.EndpointAuthorization}?client_id={_settings.Microsoft.ClientId}" +
                    $"&response_type=code&scope=openid profile email&response_mode=fragment&prompt=select_account" +
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

        var tenant = _settings.Microsoft.Tenant;
        var issuer = discovery.Issuer.Replace("{tenantid}", tenant);
        var authorization = discovery.AuthorizationEndpoint.Replace("/common/", $"/{tenant}/");
        var token = discovery.TokenEndpoint.Replace("/common/", $"/{tenant}/");

        _settings.Microsoft.Issuer = issuer;
        _settings.Microsoft.EndpointAuthorization = authorization;
        _settings.Microsoft.EndpointToken = token;

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
