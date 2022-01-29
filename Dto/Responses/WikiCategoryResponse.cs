namespace ProjectBlu.Dto.Responses;

public class WikiCategoryResponse
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public bool IsPublic { get; set; }
    public List<string> Tags { get; set; }
    public ICollection<WikiArticleResponse> Articles { get; set; }

    public int? ArticleCount { get; set; }
}
