namespace ProjectBlu.Models;

public class WikiArticle
{
    public int Id { get; set; }

    [Required, MaxLength(60)]
    public string Title { get; set; }

    public string? Contents { get; set; }

    public bool IsPublic { get; set; } = true;

    public List<string> Tags { get; set; } = new List<string>();

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int CategoryId { get; set; }
    public WikiCategory Category { get; set; }

    public int AuthorId { get; set; }
    public User Author { get; set; }
}
