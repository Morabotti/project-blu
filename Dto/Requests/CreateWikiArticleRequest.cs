namespace ProjectBlu.Dto.Requests;

public class CreateWikiArticleRequest
{
    [Required]
    public string Title { get; set; }

    public string? Contents { get; set; }

    public bool IsPublic { get; set; }

    public List<string> Tags { get; set; }

    [Required]
    public int CategoryId { get; set; }
}
