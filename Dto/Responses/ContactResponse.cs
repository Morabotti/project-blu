using ProjectBlu.Models;

namespace ProjectBlu.Dto.Responses;

public class ContactResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public LocationResponse Location { get; set; }
    public string? Title { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public DateTime CreatedAt { get; set; }
    public CustomerResponse Customer { get; set; }
}
