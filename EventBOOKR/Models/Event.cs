using System.ComponentModel.DataAnnotations;

namespace EventBOOKR.Models;

public class Event
{
    public int EventId { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    public string? Description { get; set; }

    public string? ImageUrl { get; set; }
    
    public ICollection<Booking>? Bookings { get; set; }
}