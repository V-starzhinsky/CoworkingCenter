// Controllers/MeetingRoomsController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoworkingCenter.Data;
using CoworkingCenter.Models;

namespace CoworkingCenter.Controllers
{
    [ResponseCache(Duration = 276, Location = ResponseCacheLocation.Any)]
    public class MeetingRoomsController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public MeetingRoomsController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            var rooms = await _context.MeetingRooms.ToListAsync();
            return View(rooms);
        }
        
        // Проверка доступности переговорной комнаты на заданное время
        [HttpPost]
        public async Task<IActionResult> CheckAvailability(int roomId, DateTime date, TimeSpan startTime, TimeSpan endTime)
        {
            var startDateTime = date.Add(startTime);
            var endDateTime = date.Add(endTime);
            
            // Проверка занятости (для упрощения - нет прямых бронирований переговорных в этой модели)
            var isAvailable = true; // В реальном проекте проверять по бронированиям
            
            ViewBag.RoomId = roomId;
            ViewBag.IsAvailable = isAvailable;
            
            var rooms = await _context.MeetingRooms.ToListAsync();
            return View("Index", rooms);
        }
    }
}