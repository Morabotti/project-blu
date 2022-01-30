namespace ProjectBlu.Dto.Requests;

public class CreateProjectRequest
{
    [Required]
    public string Name { get; set; }

    public string? Description { get; set; }

    [Required]
    public bool IsPublic { get; set; }

    [Required]
    public string Identifier { get; set; }

    public int? CustomerId { get; set; }
}
