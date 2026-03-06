using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventBOOKR.Data;
using EventBOOKR.Models;

namespace EventBOOKR.Controllers;

public class EventsController : Controller
{
    private readonly ApplicationDbContext _context;

    public EventsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Events.ToListAsync());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("EventId,Name,Description,ImageUrl")] Event @event)
    {
        if (string.IsNullOrWhiteSpace(@event.ImageUrl))
        {
            @event.ImageUrl = "https://via.placeholder.com/300x200?text=Event+Image";
        }
        
        if (ModelState.IsValid)
        {
            _context.Add(@event);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = $"Event '{@event.Name}' was successfully created!";
            return RedirectToAction(nameof(Index));
        }

        return View(@event);
    }
    
    // GET: Events/Details
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var eventItem = await _context.Events
            .Include(e => e.Schedules!)
                .ThenInclude(s => s.Venue)
            .FirstOrDefaultAsync(m => m.EventId == id);

        if (eventItem == null) return NotFound();
        return View(eventItem);
    }
}