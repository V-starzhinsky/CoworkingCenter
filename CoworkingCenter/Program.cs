using Microsoft.EntityFrameworkCore;
using CoworkingCenter.Data;
using CoworkingCenter.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Подключение к MSSQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        .EnableSensitiveDataLogging(builder.Environment.IsDevelopment()));

// Добавляем Response Caching
builder.Services.AddResponseCaching();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Response Caching middleware
app.UseResponseCaching();

// Инициализация БД
app.UseDbInitializer();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Residents}/{action=Index}/{id?}");

app.Run();