using System.Text.Json.Serialization;

namespace ProjectBlu.Dto.Responses;

public class DiscoveryResponse
{
    [JsonPropertyName("issuer")]
    public string Issuer { get; set; }

    [JsonPropertyName("authorization_endpoint")]
    public string AuthorizationEndpoint { get; set; }

    [JsonPropertyName("token_endpoint")]
    public string TokenEndpoint { get; set; }

    [JsonPropertyName("userinfo_endpoint")]
    public string UserinfoEndpoint { get; set; }

    [JsonPropertyName("jwks_uri")]
    public string CertificationEndpoint { get; set; }
}
