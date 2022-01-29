namespace ProjectBlu.Dto.Requests;

public class CreateWikiCategoryRequest
{
    [Required]
    public string Title { get; set; }

    public string? Description { get; set; }

    public bool IsPublic { get; set; }

    public List<string> Tags { get; set; }
}
