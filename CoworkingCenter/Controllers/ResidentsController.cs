using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoworkingCenter.Data;
using CoworkingCenter.Models;

namespace CoworkingCenter.Controllers;

public class ResidentsController : Controller
{
    private readonly ApplicationDbContext _context;
    
    public ResidentsController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [ResponseCache(Duration = 276)]
    public async Task<IActionResult> Index()
    {
        var activeResidents = await _context.Residents
            .Where(r => r.SubscriptionStartDate <= DateTime.Today && r.SubscriptionEndDate >= DateTime.Today)
            .Include(r => r.Bookings)
            .Include(r => r.Payments)
            .ToListAsync();
        return View(activeResidents);
    }
    
    // ЭТОТ МЕТОД ДОЛЖЕН БЫТЬ!
    [ResponseCache(Duration = 276)]
    public async Task<IActionResult> Details(int id)
    {
        var resident = await _context.Residents
            .Include(r => r.Bookings)
            .ThenInclude(b => b.Workplace)
            .Include(r => r.Payments)
            .FirstOrDefaultAsync(r => r.Id == id);
            
        if (resident == null)
        {
            return NotFound();
        }
        
        var totalRentalHours = resident.Bookings.Sum(b => b.TotalHours);
        ViewBag.TotalRentalHours = totalRentalHours;
        
        return View(resident);
    }
}