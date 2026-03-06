using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventBOOKR.Data;
using EventBOOKR.Models;

namespace EventBOOKR.Controllers;

public class BookingsController : Controller
{
    private readonly ApplicationDbContext _context;

    public BookingsController(ApplicationDbContext context)
    {
        _context = context;
    }

    
    // GET: Bookings
    public async Task<IActionResult> Index()
    {
        // Include pulls in the Schedule and then the related Venue and Event data so it can display their names
        var applicationDbContext = _context.Bookings
            .Include(b => b.Schedule)
                .ThenInclude(s => s.Event)
            .Include(b => b.Schedule)
                .ThenInclude(s => s.Venue);
        
        return View(await applicationDbContext.ToListAsync());
    }

    
    // GET: Bookings/Details
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var booking = await _context.Bookings
            .Include(b => b.Schedule)
                .ThenInclude(s => s.Event)
            .Include(b => b.Schedule)
                .ThenInclude(s => s.Venue)
            .FirstOrDefaultAsync(m => m.BookingId == id);

        if (booking == null) return NotFound();

        return View(booking);
    }
    
    
    // GET: Bookings/Create
    public IActionResult Create()
    {
        // Only show events in the first dropdown that have scheduled instances
        var availableEvents = _context.Events
            .Where(e => e.Schedules!.Any())
            .ToList();

        ViewData["EventId"] = new SelectList(availableEvents, "EventId", "Name");
        return View();
    }

    
    // POST: Bookings/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("BookingId,ScheduleId,ReservationTime,Pax")] Booking booking)
    {
        if (ModelState.IsValid)
        {
            _context.Add(booking);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Booking successfully confirmed!";
            return RedirectToAction(nameof(Index));
        }

        return View(booking);
    }
    
    
    // GET: Bookings/Edit
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var booking = await _context.Bookings.FindAsync(id);
        if (booking == null) return NotFound();

        return View(booking);
    }
    
    
    // POST: Bookings/Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("BookingId,ScheduleId,ReservationTime,Pax")] Booking booking)
    {
        if (id != booking.BookingId) return NotFound();

        if (ModelState.IsValid)
        {
            _context.Update(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(booking);
    }
    
    // GET: Bookings/Delete
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var booking = await _context.Bookings
            .Include(b => b.Schedule)
            .ThenInclude(s => s.Event)
            .FirstOrDefaultAsync(m => m.BookingId == id);

        if (booking == null) return NotFound();

        return View(booking);
    }
    
    // POST: Bookings/Delete
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var booking = await _context.Bookings.FindAsync(id);
        if (booking != null)
        {
            _context.Bookings.Remove(booking);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    
    // =============================================================================================================
    // API ENDPOINT FOR DYNAMIC DROPDOWNS
    // =============================================================================================================
    [HttpGet]
    public async Task<JsonResult> GetSchedulesForEvent(int eventId)
    {
        var schedules = await _context.EventSchedules
            .Include(s => s.Venue)
            .Include(s => s.Event)
            .Where(s => s.EventId == eventId)
            .OrderBy(s => s.StartTime)
            .Select(s => new
            {
                scheduleId = s.ScheduleId,
                venueName = s.Venue!.Name,
                startTime = s.StartTime.ToString("yyyy-MM-ddTHH:mm"),
                endTime = s.EndTime.ToString("yyyy-MM-ddTHH:mm"),
                isFlexible = s.Event!.IsFlexibleSchedule,
                // Pre-format string so Javascript doesn't have to
                formattedDisplay = s.Event.IsFlexibleSchedule
                    ? $"{s.Venue.Name} ({s.StartTime:MMM dd} to {s.EndTime:MMM dd})"
                    : $"{s.Venue.Name} - {s.StartTime:MMM dd @ HH:mm}"
            })
            .ToListAsync();

        return Json(schedules);
    }
}