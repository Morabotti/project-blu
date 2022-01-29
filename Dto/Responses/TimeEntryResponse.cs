using ProjectBlu.Models;

namespace ProjectBlu.Dto.Responses;

public class TimeEntryResponse
{
    public int Id { get; set; }
    public string? Comment { get; set; }
    public decimal Hours { get; set; }

    public DateOnly Date { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public IssueResponse Issue { get; set; }
    public ProjectResponse Project { get; set; }
    public UserResponse User { get; set; }
}
