using ProjectBlu.Models;

namespace ProjectBlu.Dto.Responses;

public class CustomerResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public LocationResponse Location { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Website { get; set; }

    public DateTime CreatedAt { get; set; }

    public ICollection<ContactResponse> Contacts { get; set; }
    public ICollection<DealResponse> Deals { get; set; }
    public ICollection<ProjectResponse> Projects { get; set; }
}
