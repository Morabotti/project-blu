using ProjectBlu.Models;

namespace ProjectBlu.Services.Interfaces;

public interface IOIDCService
{
    AuthorizationResponse CreateAuthorizationUrl(string provider);
    Task<User> LoginAsync(string provider, string code, string nonce);
    Dictionary<string, bool> GetProviders();
    bool IsValidProvider(string provider);
}
