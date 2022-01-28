namespace ProjectBlu.Models;

public class IssueCategory
{
    public long Id { get; set; }

    [Required, MaxLength(40)]
    public string Name { get; set; }

    [Required]
    public bool IsDefault { get; set; }

    public int? Position { get; set; }
}
