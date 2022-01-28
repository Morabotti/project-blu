namespace ProjectBlu.Models;

public class News
{
    public int Id { get; set; }

    [Required, MaxLength(120)]
    public string Subject { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int AuthorId { get; set; }
    public User Author { get; set; }
}
