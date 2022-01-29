namespace ProjectBlu.Dto.Responses;

public class WikiArticleResponse
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Contents { get; set; }
    public bool IsPublic { get; set; }
    public List<string> Tags { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public WikiCategoryResponse Category { get; set; }
    public AuthorResponse Author { get; set; }
}
