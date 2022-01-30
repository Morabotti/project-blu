namespace ProjectBlu.Models;

public enum DealStatus
{
    Cold = 0,
    Contacted = 1,
    Negotiations = 2,
    Won = 3,
    Lost = 4
}

public class Deal
{
    public int Id { get; set; }

    [Required, MaxLength(60)]
    public string Name { get; set; }

    [Precision(precision: 10, scale: 2)]
    public decimal? Price { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? DueDate { get; set; }

    public int? CustomerId { get; set; }
    public Customer Customer { get; set; }

    public int? ResponsibleId { get; set; }
    public User Responsible { get; set; }
}
