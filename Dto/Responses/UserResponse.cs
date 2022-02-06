using ProjectBlu.Models;

namespace ProjectBlu.Dto.Responses;

public class UserResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public UserImageResponse Image { get; set; }
    public LocationResponse Location { get; set; }
    public UserRole Role { get; set; }
    public AuthProvider? Provider { get; set; }
    public DateTime CreatedAt { get; set; }
}
