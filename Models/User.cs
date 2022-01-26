namespace ProjectBlu.Models;

public enum UserRole
{
    User = 0,
    Admin = 1
}

public class User
{
    public long Id { get; set; }

    [Required]
    [MaxLength(30)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(30)]
    public string LastName { get; set; }

    [Required]
    [MaxLength(60)]
    public string Email { get; set; }

    [MaxLength(120)]
    public string? Password { get; set; }

    [DefaultValue(UserRole.User)]
    public UserRole Role { get; set; }

    public DateTime CreatedAt { get; set; }
}
