using ProjectBlu.Models;

namespace ProjectBlu.Dto.Responses;

public class DealResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal? Price { get; set; }
    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? DueDate { get; set; }

    public CustomerResponse Customer { get; set; }
    public AuthorResponse Responsible { get; set; }
}
