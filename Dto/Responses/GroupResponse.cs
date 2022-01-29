using ProjectBlu.Models;

namespace ProjectBlu.Dto.Responses;

public class GroupResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool Assignable { get; set; }
    public int? Position { get; set; }
    public List<string> Permissions { get; set; }
}
