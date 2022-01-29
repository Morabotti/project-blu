using ProjectBlu.Models;

namespace ProjectBlu.Dto.Responses;

public class IssueResponse
{
    public int Id { get; set; }
    public string Subject { get; set; }
    public string? Description { get; set; }
    public int DoneRatio { get; set; }
    public decimal? EstimatedTime { get; set; }
    public IssuePriority Priority { get; set; }

    public DateTime? DueDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public ProjectResponse Project { get; set; }
    public IssueStatusResponse Status { get; set; }
    public IssueCategoryResponse Category { get; set; }
    public AuthorResponse Author { get; set; }
    public AuthorResponse Assigned { get; set; }

    public ICollection<TimeEntryResponse> TimeEntries { get; set; }
}
