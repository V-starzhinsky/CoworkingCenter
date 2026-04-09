using Microsoft.EntityFrameworkCore;
using CoworkingCenter.Data;
using CoworkingCenter.Models;

namespace CoworkingCenter.Middleware;

public class DbInitializerMiddleware
{
    private readonly RequestDelegate _next;
    
    public DbInitializerMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext context, ApplicationDbContext dbContext)
    {
        try
        {
            // Создаем БД если её нет
            await dbContext.Database.EnsureCreatedAsync();
            
            // Применяем миграции
            await dbContext.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database error: {ex.Message}");
        }
        
        await InitializeDatabaseAsync(dbContext);
        await _next(context);
    }
    
    private async Task InitializeDatabaseAsync(ApplicationDbContext dbContext)
    {
        // Проверяем, есть ли уже данные
        if (await dbContext.Residents.AnyAsync())
            return;
            
        // Добавление тестовых данных
        var residents = new List<Resident>
        {
            new() { FullNameOrCompany = "Иван Петров", Contacts = "ivan@example.com", Tariff = "Безлимитный", SubscriptionStartDate = DateTime.Today.AddMonths(-1), SubscriptionEndDate = DateTime.Today.AddMonths(5) },
            new() { FullNameOrCompany = "ООО ТехноПро", Contacts = "contact@technopro.ru", Tariff = "Корпоративный", SubscriptionStartDate = DateTime.Today.AddMonths(-2), SubscriptionEndDate = DateTime.Today.AddMonths(4) },
            new() { FullNameOrCompany = "Мария Сидорова", Contacts = "+7 999 123-45-67", Tariff = "Стандарт", SubscriptionStartDate = DateTime.Today.AddMonths(-3), SubscriptionEndDate = DateTime.Today.AddMonths(3) },
            new() { FullNameOrCompany = "Алексей Козлов", Contacts = "alex@kozlov.ru", Tariff = "Базовый", SubscriptionStartDate = DateTime.Today.AddDays(-10), SubscriptionEndDate = DateTime.Today.AddMonths(2) },
            new() { FullNameOrCompany = "Студия Дизайн", Contacts = "info@design.studio", Tariff = "Безлимитный", SubscriptionStartDate = DateTime.Today.AddMonths(-1), SubscriptionEndDate = DateTime.Today.AddMonths(7) }
        };
        
        var workplaces = new List<Workplace>
        {
            new() { Type = "фикс", TableNumber = "A101", Equipment = "Монитор 24\", Клавиатура, Мышь", PricePerHour = 200m, PricePerDay = 1000m, IsAvailable = true },
            new() { Type = "флекс", TableNumber = "B202", Equipment = "Ноутбук, Подставка", PricePerHour = 150m, PricePerDay = 800m, IsAvailable = true },
            new() { Type = "офис", TableNumber = "C303", Equipment = "Проектор, Доска, 4 стола", PricePerHour = 500m, PricePerDay = 3000m, IsAvailable = true },
            new() { Type = "фикс", TableNumber = "A102", Equipment = "Монитор 27\", Клавиатура, Мышь", PricePerHour = 250m, PricePerDay = 1200m, IsAvailable = true },
            new() { Type = "флекс", TableNumber = "B203", Equipment = "Стандартное", PricePerHour = 120m, PricePerDay = 600m, IsAvailable = true }
        };
        
        var meetingRooms = new List<MeetingRoom>
        {
            new() { Name = "Переговорная Альфа", Capacity = 6, Equipment = "Проектор, Доска, Конференц-связь", PricePerHour = 1000m, IsAvailable = true },
            new() { Name = "Переговорная Бета", Capacity = 12, Equipment = "Экран 75\", Доска, Флипчарт", PricePerHour = 2000m, IsAvailable = true },
            new() { Name = "Переговорная Гамма", Capacity = 4, Equipment = "Монитор, Доска", PricePerHour = 800m, IsAvailable = true }
        };
        
        await dbContext.Residents.AddRangeAsync(residents);
        await dbContext.Workplaces.AddRangeAsync(workplaces);
        await dbContext.MeetingRooms.AddRangeAsync(meetingRooms);
        await dbContext.SaveChangesAsync();
        
        var bookings = new List<Booking>
        {
            new() { Date = DateTime.Today, StartTime = DateTime.Today.AddHours(10), EndTime = DateTime.Today.AddHours(12), ResidentId = 1, WorkplaceId = 1, TotalCost = 400m },
            new() { Date = DateTime.Today.AddDays(1), StartTime = DateTime.Today.AddDays(1).AddHours(14), EndTime = DateTime.Today.AddDays(1).AddHours(17), ResidentId = 2, WorkplaceId = 2, TotalCost = 450m },
            new() { Date = DateTime.Today.AddDays(2), StartTime = DateTime.Today.AddDays(2).AddHours(9), EndTime = DateTime.Today.AddDays(2).AddHours(18), ResidentId = 3, WorkplaceId = 3, TotalCost = 3000m }
        };
        
        var payments = new List<Payment>
        {
            new() { Date = DateTime.Today.AddDays(-5), Amount = 5000m, Service = "Абонемент на месяц", ResidentId = 1 },
            new() { Date = DateTime.Today.AddDays(-10), Amount = 15000m, Service = "Корпоративный абонемент", ResidentId = 2 },
            new() { Date = DateTime.Today.AddDays(-3), Amount = 3000m, Service = "Аренда переговорной", ResidentId = 1 }
        };
        
        await dbContext.Bookings.AddRangeAsync(bookings);
        await dbContext.Payments.AddRangeAsync(payments);
        await dbContext.SaveChangesAsync();
        
        Console.WriteLine("Database initialized successfully!");
    }
}

public static class DbInitializerMiddlewareExtensions
{
    public static IApplicationBuilder UseDbInitializer(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<DbInitializerMiddleware>();
    }
}