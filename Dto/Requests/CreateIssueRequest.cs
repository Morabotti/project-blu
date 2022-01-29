using ProjectBlu.Models;

namespace ProjectBlu.Dto.Requests;

public class CreateIssueRequest
{
    public string Subject { get; set; }
    public string? Description { get; set; }
    public decimal? EstimatedTime { get; set; }
    public IssuePriority Priority { get; set; }
    public DateTime? DueDate { get; set; }
    public long ProjectId { get; set; }
    public long StatusId { get; set; }
    public long CategoryId { get; set; }
    public long? AssignedId { get; set; }
}
