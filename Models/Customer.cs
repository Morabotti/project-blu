using ProjectBlu.Models.Owned;

namespace ProjectBlu.Models;

public class Customer
{
    public int Id { get; set; }

    [Required, MaxLength(60)]
    public string Name { get; set; }

    public Location Location { get; set; } = new Location();

    [MaxLength(15)]
    public string? Phone { get; set; }

    [EmailAddress, MaxLength(60)]
    public string? Email { get; set; }

    public string? Website { get; set; }

    public DateTime CreatedAt { get; set; }

    public ICollection<Contact> Contacts { get; set; }
    public ICollection<Deal> Deals { get; set; }
    public ICollection<Project> Projects { get; set; }
}
