namespace ProjectBlu.Models;

public class Member
{
    public int UserId { get; set; }
    public User User { get; set; }

    public int GroupId { get; set; }
    public Group Group { get; set; }

    public int ProjectId { get; set; }
    public Project Project { get; set; }

    public DateTime CreatedAt { get; set; }
}
