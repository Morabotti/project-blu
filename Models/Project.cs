namespace ProjectBlu.Models;

public class Project
{
    public int Id { get; set; }

    [Required, MaxLength(30)]
    public string Name { get; set; }

    public string? Description { get; set; }

    [Required]
    public bool IsPublic { get; set; }

    [Required, MaxLength(50)]
    public string Identifier { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int? CustomerId { get; set; }
    public Customer Customer { get; set; }

    public ICollection<Issue> Issues { get; set; }
    public ICollection<Document> Documents { get; set; }
    public ICollection<TimeEntry> TimeEntries { get; set; }
    public ICollection<Member> Members { get; set; }
}
