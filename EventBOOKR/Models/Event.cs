using System.ComponentModel.DataAnnotations;

namespace EventBOOKR.Models;

public class Event
{
    [Key]
    public int EventId { get; set; }
    
    [Required]
    public required string Name { get; set; }
    
    public string? Description { get; set; }

    public string? ImageUrl { get; set; }
    
    // TRUE: Multi-day event where users pick timeslot in divisions of 30min (e.g., an expo running the whole weekend)
    // FALSE: A strictly scheduled event (e.g., a Quiz Night at 19:00)
    public bool IsFlexibleSchedule { get; set; }
    
    public ICollection<EventSchedule>? Schedules { get; set; }
}