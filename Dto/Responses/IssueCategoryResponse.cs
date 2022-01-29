namespace ProjectBlu.Dto.Responses;

public class IssueCategoryResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsDefault { get; set; }
    public int? Position { get; set; }
}
