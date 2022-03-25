using ProjectBlu.Models;
using System.Text.Json.Serialization;

namespace ProjectBlu.Dto.Responses;

public class UserResponse
{
    public string Id { get; set; }

    [JsonIgnore]
    public int DecodeId { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public UserImageResponse Image { get; set; }
    public LocationResponse Location { get; set; }
    public UserRole Role { get; set; }
    public AuthProvider? Provider { get; set; }
    public DateTime CreatedAt { get; set; }
}
