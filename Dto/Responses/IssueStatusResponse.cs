namespace ProjectBlu.Dto.Responses;

public class IssueStatusResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsDefault { get; set; }
    public bool IsClosed { get; set; }
    public int? Position { get; set; }
    public int? DoneRatio { get; set; }
}
