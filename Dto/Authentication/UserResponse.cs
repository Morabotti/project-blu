using ProjectBlu.Models;

namespace ProjectBlu.Dto.Authentication;

public class UserResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public UserRole Role { get; set; }
    public AuthProvider? Provider { get; set; }
    public DateTime CreatedAt { get; set; }
}
