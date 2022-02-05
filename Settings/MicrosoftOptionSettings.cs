namespace ProjectBlu.Settings;

public class MicrosoftOptionSettings
{
    public string? ClientId { get; set; }
    public string? ClientSecret { get; set; }
    public string? Tenant { get; set; }
    public string? DiscoveryUrl { get; set; }

    // Populated through discovery url
    public string? Issuer { get; set; }
    public string? EndpointAuthorization { get; set; }
    public string? EndpointToken { get; set; }

    public bool HasInitialValues()
    {
        return ClientId != null  && ClientId != ""
            && ClientSecret != null && ClientSecret != ""
            && Tenant != null && Tenant != ""
            && DiscoveryUrl != null && DiscoveryUrl != "";
    }

    public bool HasFullValues()
    {
        return HasInitialValues() && Issuer != null && EndpointAuthorization != null && EndpointToken != null;
    }
}
