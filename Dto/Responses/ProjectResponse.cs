using ProjectBlu.Models;

namespace ProjectBlu.Dto.Responses;

public class ProjectResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool IsPublic { get; set; }
    public string Identifier { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public CustomerResponse Customer { get; set; }

    public ICollection<IssueResponse> Issues { get; set; }
    public ICollection<DocumentResponse> Documents { get; set; }
    public ICollection<TimeEntryResponse> TimeEntries { get; set; }
    public ICollection<MemberResponse> Members { get; set; }
}
