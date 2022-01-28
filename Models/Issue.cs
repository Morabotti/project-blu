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
    public long Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Subject { get; set; }

    public string? Description { get; set; }

    [DefaultValue(0)]
    public int DoneRatio { get; set; }

    [Precision(precision: 10, scale: 2)]
    public decimal? EstimatedTime { get; set; }

    public IssuePriority Priority { get; set; } = IssuePriority.Medium;

    public DateTime? DueDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public long ProjectId { get; set; }
    public Project Project { get; set; }

    public long StatusId { get; set; }
    public IssueStatus Status { get; set; }

    public long? CategoryId { get; set; }
    public IssueCategory Category { get; set; }

    public long AuthorId { get; set; }
    public User Author { get; set; }

    public long? AssignedId { get; set; }
    public User Assigned { get; set; }
}
