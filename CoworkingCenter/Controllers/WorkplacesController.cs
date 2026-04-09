// Controllers/WorkplacesController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoworkingCenter.Data;
using CoworkingCenter.Models;

namespace CoworkingCenter.Controllers
{
    [ResponseCache(Duration = 276, Location = ResponseCacheLocation.Any)]
    public class WorkplacesController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public WorkplacesController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            var workplaces = await _context.Workplaces
                .Include(w => w.Bookings)
                .ToListAsync();
            return View(workplaces);
        }
        
        public async Task<IActionResult> Details(int id)
        {
            var workplace = await _context.Workplaces
                .Include(w => w.Bookings)
                .ThenInclude(b => b.Resident)
                .FirstOrDefaultAsync(w => w.Id == id);
                
            if (workplace == null)
                return NotFound();
                
            return View(workplace);
        }
    }
}