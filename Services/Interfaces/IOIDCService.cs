namespace ProjectBlu.Services.Interfaces;

public interface IOIDCService
{
    AuthorizationResponse CreateAuthorizationUrl(string provider);
    bool IsValidProvider(string provider);
}
