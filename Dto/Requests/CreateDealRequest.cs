namespace ProjectBlu.Dto.Requests;

public class CreateDealRequest
{
    [Required]
    public string Name { get; set; }

    public decimal? Price { get; set; }

    public string? Description { get; set; }

    public DateTime? DueDate { get; set; }

    public int CustomerId { get; set; }

    public int? ResponsibleId { get; set; }
}
