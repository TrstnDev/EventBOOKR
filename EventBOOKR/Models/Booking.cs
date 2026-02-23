using System.ComponentModel.DataAnnotations;

namespace EventBOOKR.Models;

public class Booking
{
    public int BookingId { get; set; }
    
    [Required]
    public int VenueId { get; set; }
    public Venue? Venue { get; set; }
    
    [Required]
    public int EventId { get; set; }
    public Event? Event { get; set; }
    
    [Required]
    public DateTime StartDate { get; set; }
    
    [Required]
    public DateTime EndDate { get; set; }
}