using ProjectBlu.Models.Owned;

namespace ProjectBlu.Models;

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

    public Location Location { get; set; } = new Location();

    [Required, EmailAddress, MaxLength(60)]
    public string Email { get; set; }

    [MaxLength(120)]
    public string? Password { get; set; }

    public UserRole Role { get; set; } = UserRole.User;

    public DateTime CreatedAt { get; set; }

    public ICollection<Deal> ResponsibleDeals { get; set; }
    public ICollection<Member> Members { get; set; }
    public ICollection<WikiArticle> Articles { get; set; }
    public ICollection<TimeEntry> TimeEntries { get; set; }
}
