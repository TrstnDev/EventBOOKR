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
            return RedirectToAction(nameof(Index));
        }

        return View(@event);
    }
}