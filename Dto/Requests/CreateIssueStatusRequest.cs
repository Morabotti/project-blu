namespace ProjectBlu.Dto.Requests;

public class CreateIssueStatusRequest
{
    [Required]
    public string Name { get; set; }

    [Required]
    public bool IsDefault { get; set; }

    [Required]
    public bool IsClosed { get; set; }

    public int? Position { get; set; }

    public int? DoneRatio { get; set; }
}
