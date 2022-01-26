namespace ProjectBlu.Models;

public class Issue
{
    public long Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Subject { get; set; }

    public string? Description { get; set; }

    [DefaultValue(0)]
    public int DoneRatio { get; set; }

    public long? EstimatedTime { get; set; }

    public DateTime? DueDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public long ProjectId { get; set; }
    public Project Project { get; set; }
}
