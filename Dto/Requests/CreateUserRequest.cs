using ProjectBlu.Models;

namespace ProjectBlu.Dto.Requests;

public class CreateUserRequest
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    public LocationResponse Location { get; set; } = new LocationResponse();
    public UserRole Role { get; set; } = UserRole.User;
}
