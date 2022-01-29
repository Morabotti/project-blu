namespace ProjectBlu.Models;

public class Group
{
    public int Id { get; set; }
    
    [Required, MaxLength(40)]
    public string Name { get; set; }

    [Required]
    public bool Assignable { get; set; }

    public int? Position { get; set; }

    // TODO: Add and validate permissions
    public List<string> Permissions { get; set; } = new List<string>();

    public ICollection<Member> Members { get; set; }
}
