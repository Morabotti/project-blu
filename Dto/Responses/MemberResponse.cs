using ProjectBlu.Models;

namespace ProjectBlu.Dto.Responses;

public class MemberResponse
{
    public AuthorResponse User { get; set; }
    public GroupResponse Group { get; set; }
    public ProjectResponse Project { get; set; }

    public DateTime CreatedAt { get; set; }
}
