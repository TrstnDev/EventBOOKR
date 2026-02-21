using System.ComponentModel.DataAnnotations;

namespace EventBOOKR.Models;

public class Event
{
    public int EventId { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    public string Description { get; set; }

    public string ImageUrl { get; set; } = "https://via.placeholder.com/300x200?text=Event+Image";
    
    public ICollection<Booking> Bookings { get; set; }
}