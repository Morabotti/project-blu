using ProjectBlu.Models;

namespace ProjectBlu.Dto.Responses;

public class AuthorizationResponse
{
    public string State { get; set; }
    public string Nonce { get; set; }
    public string Redirect { get; set; }
}
