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

    public async Task<IActionResult> Index()
    {
        // Include pulls in the related Venue and Event data so it can display their names
        var applicationDbContext = _context.Bookings.Include(b => b.Event).Include(b => b.Venue);
        return View(await applicationDbContext.ToListAsync());
    }

    public IActionResult Create()
    {
        // Populates the dropdown lists for the form
        ViewData["EventId"] = new SelectList(_context.Events, "EventId", "Name");
        ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "Name");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("BookingId,VenueId,EventId,StartDate,EndDate")] Booking booking)
    {
        // 1. Check if EndDate is before StartDate
        if (booking.EndDate <= booking.StartDate)
        {
            ModelState.AddModelError("EndDate", "End date must be after the start date.");
        }
        
        // 2. Prevent double bookings logic
        bool isDoubleBooked = await _context.Bookings.AnyAsync(b =>
            b.VenueId == booking.VenueId &&
            ((booking.StartDate >= b.StartDate && booking.StartDate < b.EndDate) ||
             (booking.EndDate > b.StartDate && booking.EndDate <= b.EndDate) ||
             (booking.StartDate <= b.StartDate && booking.EndDate >= b.EndDate)));

        if (isDoubleBooked)
        {
            ModelState.AddModelError(string.Empty, "This venue is already booked during the selected timeframe.");
        }

        if (ModelState.IsValid)
        {
            _context.Add(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        // If code reaches this point, something failed: re-populate dropdowns and return form
        ViewData["EventId"] = new SelectList(_context.Events, "EventId", "Name", booking.EventId);
        ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "Name", booking.VenueId);
        return View(booking);
    }
}