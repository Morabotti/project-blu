namespace ProjectBlu.Dto.Requests;

public class CreateGroupRequest
{
    [Required]
    public string Name { get; set; }

    [Required]
    public bool Assignable { get; set; }

    public int? Position { get; set; }

    [Required]
    public List<string> Permissions { get; set; }
}
