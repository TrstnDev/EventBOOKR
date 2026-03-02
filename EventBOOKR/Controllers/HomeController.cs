using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EventBOOKR.Models;
using EventBOOKR.Data;
using Microsoft.EntityFrameworkCore;

namespace EventBOOKR.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<IActionResult> Index()
    {
        // Using viewbag to pass temporary data to the View
        ViewBag.TotalVenues = await _context.Venues.CountAsync();
        ViewBag.TotalEvents = await _context.Events.CountAsync();
        ViewBag.TotalBookings = await _context.Bookings.CountAsync();
        
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}