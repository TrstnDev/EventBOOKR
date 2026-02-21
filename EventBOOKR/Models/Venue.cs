using System.ComponentModel.DataAnnotations;

namespace EventBOOKR.Models;

public class Venue
{
    public int VenueId { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Location { get; set; }
    
    [Required]
    public int Capacity { get; set; }

    public string ImageUrl { get; set; } = "https://via.placeholder.com/300x200?text=Venue+Image";
    
    public ICollection<Booking> Bookings { get; set; }
}