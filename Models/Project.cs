namespace ProjectBlu.Models;

public class Project
{
    public long Id { get; set; }

    [Required]
    [MaxLength(30)]
    public string Name { get; set; }

    public string? Description { get; set; }

    [Required]
    public bool IsPublic { get; set; }

    [Required]
    [MaxLength(50)]
    public string Identifier { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public List<Issue> Issues { get; set; }
    public List<Document> Documents { get; set; }
}
