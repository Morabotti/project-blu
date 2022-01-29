using ProjectBlu.Models;

namespace ProjectBlu.Dto.Responses;

public class DocumentResponse
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public ProjectResponse Project { get; set; }
}
