using ProjectBlu.Models.Owned;

namespace ProjectBlu.Models;

public class TimeEntry
{
    public long Id { get; set; }

    [MaxLength(255)]
    public string? Comment { get; set; }

    [Required, Precision(precision: 10, scale: 2)]
    public decimal Hours { get; set; }

    [Required]
    public DateOnly Date { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
