namespace ProjectBlu.Models.Owned;

[Owned]
public class Location
{
    [MaxLength(30)]
    public string? Address { get; set; }

    [MaxLength(20)]
    public string? City { get; set; }

    [MaxLength(10)]
    public string? Zip { get; set; }

    [MaxLength(20)]
    public string? State { get; set; }

    [MaxLength(50)]
    public string? Country { get; set; }
}
