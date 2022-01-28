﻿using ProjectBlu.Models.Owned;

namespace ProjectBlu.Models;

public class Contact
{
    public int Id { get; set; }

    [Required, MaxLength(30)]
    public string FirstName { get; set; }

    [Required, MaxLength(30)]
    public string LastName { get; set; }

    public Location Location { get; set; } = new Location();

    [MaxLength(60)]
    public string? Title { get; set; }

    [MaxLength(15)]
    public string? Phone { get; set; }

    [EmailAddress, MaxLength(60)]
    public string? Email { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
}
