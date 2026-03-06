using System.ComponentModel.DataAnnotations;

namespace EventBOOKR.Models;

public class EventSchedule
{
    [Key]
    public int ScheduleId { get; set; }
    
    // Links to the Event
    public int EventId { get; set; }
    public Event? Event { get; set; }
    
    // Links to the Venue
    public int VenueId { get; set; }
    public Venue? Venue { get; set; }
    
    // If fixed: the exact start/end time of the event
    // if flexible: the opening and closing dates of the entire expo/event window
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    
    //Navigation Property
    public ICollection<Booking>? Bookings { get; set; }
}