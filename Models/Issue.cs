namespace ProjectBlu.Models;

public enum IssuePriority
{
    Lowest = 0,
    Low = 1,
    Medium = 2,
    High = 3,
    Highest = 4
}

public class Issue
{
    public int Id { get; set; }

    [Required, MaxLength(255)]
    public string Subject { get; set; }

    public string? Description { get; set; }

    public int DoneRatio { get; set; } = 0;

    [Precision(precision: 10, scale: 2)]
    public decimal? EstimatedTime { get; set; }

    public IssuePriority Priority { get; set; } = IssuePriority.Medium;

    public DateTime? DueDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int ProjectId { get; set; }
    public Project Project { get; set; }

    public int StatusId { get; set; }
    public IssueStatus Status { get; set; }

    public int? CategoryId { get; set; }
    public IssueCategory Category { get; set; }

    public int AuthorId { get; set; }
    public User Author { get; set; }

    public int? AssignedId { get; set; }
    public User Assigned { get; set; }

    public ICollection<TimeEntry> TimeEntries { get; set; }
}
