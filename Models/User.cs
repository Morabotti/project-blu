using ProjectBlu.Models.Owned;
using System.Text.Json.Serialization;

namespace ProjectBlu.Models;

public enum AuthProvider
{
    Microsoft = 1,
    Google = 2
}

public enum UserRole
{
    User = 0,
    Admin = 1
}

public class User
{
    public int Id { get; set; }

    [Required, MaxLength(30)]
    public string FirstName { get; set; }

    [Required, MaxLength(30)]
    public string LastName { get; set; }

    private Location _location;
    public Location Location { get => _location ??= new Location(); set => _location = value; }

    [Required, EmailAddress, MaxLength(60)]
    public string Email { get; set; }

    [MaxLength(120), JsonIgnore]
    public string? Password { get; set; }

    public AuthProvider? Provider { get; set; } = null;

    public UserRole Role { get; set; } = UserRole.User;

    public DateTime CreatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public ICollection<Deal> ResponsibleDeals { get; set; }
    public ICollection<Member> Members { get; set; }
    public ICollection<WikiArticle> Articles { get; set; }
    public ICollection<TimeEntry> TimeEntries { get; set; }
}
