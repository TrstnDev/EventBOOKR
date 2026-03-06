using System.ComponentModel.DataAnnotations;

namespace EventBOOKR.Models;

public class Venue
{
    public int VenueId { get; set; }
    
    [Required]
    public required string Name { get; set; }
    
    [Required]
    public required string Location { get; set; }
    
    [Required]
    public int Capacity { get; set; }

    public string? ImageUrl { get; set; }
    
    public ICollection<EventSchedule>? Schedules { get; set; }
}