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
    
    // GET: Venues/Edit
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var venue = await _context.Venues.FindAsync(id);
        if (venue == null) return NotFound();

        return View(venue);
    }
    
    // GET: Venues/Delete
    // The saveChangesError parameter lets us know if a deletion attempt failed
    public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
    {
        if (id == null) return NotFound();

        var venue = await _context.Venues.FirstOrDefaultAsync(m => m.VenueId == id);
        if (venue == null) return NotFound();

        if (saveChangesError.GetValueOrDefault())
        {
            ViewData["ErrorMessage"] =
                "Delete failed. This venue cannot be deleted because it is associated with one or more existing bookings.";
        }

        return View(venue);
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
            TempData["SuccessMessage"] = $"Venue '{venue.Name}' was successfully created!";
            return RedirectToAction(nameof(Index));
        }

        return View(venue);
    }
    
    // POST: Venues/Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("VenueId,Name,Location,Capacity,ImageUrl")] Venue venue)
    {
        if (id != venue.VenueId) return NotFound();

        if (string.IsNullOrWhiteSpace(venue.ImageUrl))
        {
            venue.ImageUrl = "https://via.placeholder.com/300x200?text=Venue+Image";
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(venue);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VenueExists(venue.VenueId)) return NotFound();
                else throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(venue);
    }
    
    // POST: Venues/Delete
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var venue = await _context.Venues.FindAsync(id);
        if (venue != null)
        {
            try
            {
                _context.Venues.Remove(venue);
                await _context.SaveChangesAsync(); // This will fail if there are bookings
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException) //Catches the SQL restriction error
            {
                // Redirect back to the GET Delete page and flag error
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        return RedirectToAction(nameof(Index));
    }

    private bool VenueExists(int id)
    {
        return _context.Venues.Any(e => e.VenueId == id);
    }

}