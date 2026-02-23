using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventBOOKR.Data;
using EventBOOKR.Models;

namespace EventBOOKR.Controllers;

public class VenuesController : Controller
{
    private readonly ApplicationDbContext _context;

    public VenuesController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    // GET: Venues (Displays the list of venues)
    public async Task<IActionResult> Index()
    {
        return View(await _context.Venues.ToListAsync());
    }
    
    // GET: Venues/Create (Displays the form)
    public IActionResult Create()
    {
        return View();
    }
    
    // POST: Venues/Create (Saves form data to the database)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("VenueId,Name,Location,Capacity,ImageUrl")] Venue venue)
    {
        if (string.IsNullOrWhiteSpace(venue.ImageUrl))
        {
            venue.ImageUrl = "https://via.placeholder.com/300x200?text=Event+Image";
        }
        
        if (ModelState.IsValid)
        {
            _context.Add(venue);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(venue);
    }

}