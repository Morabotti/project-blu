namespace ProjectBlu.Models;

public class WikiCategory
{
    public int Id { get; set; }

    [Required, MaxLength(60)]
    public string Title { get; set; }

    public string? Description { get; set; }

    public bool IsPublic { get; set; } = true;

    public List<string> Tags { get; set; } = new List<string>();

    public ICollection<WikiArticle> Articles { get; set; }
}
