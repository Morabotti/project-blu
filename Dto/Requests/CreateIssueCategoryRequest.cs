namespace ProjectBlu.Dto.Requests;

public class CreateIssueCategoryRequest
{
    [Required]
    public string Name { get; set; }

    [Required]
    public bool IsDefault { get; set; }

    public int? Position { get; set; }
}
