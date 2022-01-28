namespace ProjectBlu.Models;

public class IssueStatus
{
    public long Id { get; set; }

    [Required]
    [MaxLength(40)]
    public string Name { get; set; }

    [Required]
    public bool IsDefault { get; set; }

    [Required]
    public bool IsClosed { get; set; }

    public int? Position { get; set; }

    public int? DoneRatio { get; set; }
}
