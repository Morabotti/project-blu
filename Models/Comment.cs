namespace ProjectBlu.Models;

public enum CommentType
{
    Issue = 0,
    User = 1,
    Customer = 2,
    Project = 3,
    News = 4,
    Wiki = 5
}

[Index(nameof(CommentedId), nameof(Type))]
public class Comment
{
    public int Id { get; set; }

    [Required]
    public CommentType Type { get; set; }

    [Required]
    public int CommentedId { get; set; }

    public string? Content { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
