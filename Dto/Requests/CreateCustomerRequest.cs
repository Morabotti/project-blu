namespace ProjectBlu.Dto.Requests;

public class CreateCustomerRequest
{
    [Required]
    public string Name { get; set; }

    public LocationResponse Location { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Website { get; set; }
}
