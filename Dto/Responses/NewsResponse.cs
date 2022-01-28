namespace ProjectBlu.Dto.Responses;

public class NewsResponse
{
    public int Id { get; set; }
    public string Subject { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public AuthorResponse? Author { get; set; }
}
