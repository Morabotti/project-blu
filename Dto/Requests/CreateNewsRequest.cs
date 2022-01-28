namespace ProjectBlu.Dto.Requests;

public class CreateNewsRequest
{
    [Required]
    public string Subject { get; set; }
    public string? Description { get; set; }
}
