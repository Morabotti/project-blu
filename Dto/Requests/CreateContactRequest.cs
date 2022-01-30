namespace ProjectBlu.Dto.Requests;

public class CreateContactRequest
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    public LocationResponse Location { get; set; }

    public string? Title { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public int CustomerId { get; set; }
}
