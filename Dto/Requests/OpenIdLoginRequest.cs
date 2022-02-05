namespace ProjectBlu.Dto.Requests;

public class OpenIdLoginRequest
{
    [Required]
    public string Code { get; set; }

    [Required]
    public string State { get; set; }
}
