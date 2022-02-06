using ProjectBlu.Models.Owned;

namespace ProjectBlu.Models;

public class ImageAsset
{
    public int Id { get; set; }

    [Required, MaxLength(40)]
    public string FileName { get; set; }

    [Required, MaxLength(30)]
    public string ContentType { get; set; }

    [Required, MaxLength(255)]
    public string Source { get; set; }

    [MaxLength(63)]
    public string? Hash { get; set; }
}
