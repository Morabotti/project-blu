namespace ProjectBlu.Models;

public class Document
{
    public int Id { get; set; }

    [Required, MaxLength(60)]
    public string Title { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public int ProjectId { get; set; }
    public Project Project { get; set; }
}
