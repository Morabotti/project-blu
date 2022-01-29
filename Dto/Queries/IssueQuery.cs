using ProjectBlu.Models;

namespace ProjectBlu.Dto.Queries;

public class IssueQuery
{
    public string? Search { get; set; }
    public int? ProjectId { get; set; }
    public int? AuthorId { get; set; }
    public int? AssignedId { get; set; }
    public int? CategoryId { get; set; }
    public int? StatusId { get; set; }
    public IssuePriority? Priority { get; set; }
    public DateTime? Date { get; set; }
}
