using System.ComponentModel.DataAnnotations;

namespace EventBOOKR.Models;

public class Booking
{
    [Key]
    public int BookingId { get; set; }
    
    // Links to specific scheduled instance
    [Required]
    public int ScheduleId { get; set; }
    public EventSchedule? Schedule { get; set; }
    
    // Specific time user is attending
    // For fixed events this matches the Schedule.StartTime
    // For flexible events this is the specific 30-min interval they selected
    public DateTime ReservationTime { get; set; }
    
    [Range(1, 100, ErrorMessage = "Pax must be at least 1.")]
    public int Pax { get; set; }
}