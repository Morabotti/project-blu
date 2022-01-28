namespace ProjectBlu.Models;

public enum AttachmentType
{
    Issue = 0,
    User = 1,
    Customer = 2,
    Project = 3,
    News = 4,
    Wiki = 5
}

[Index(nameof(AttachedId), nameof(Type))]
public class Attachment
{
    public int Id { get; set; }

    [Required]
    public AttachmentType Type { get; set; }

    [Required]
    public int AttachedId { get; set; }

    [Required, MaxLength(255)]
    public string Filename { get; set; }

    [Required, MaxLength(255)]
    public string DiskName { get; set; }

    [MaxLength(255)]
    public string? Description { get; set; }

    [Required]
    public int Size { get; set; }

    public string? ContentType { get; set; }

    [Required, MaxLength(40)]
    public string Digest { get; set; }

    public int Downloads { get; set; } = 0;

    public int AuthorId { get; set; }
    public User Author { get; set; }

    public DateTime CreatedAt { get; set; }
}
