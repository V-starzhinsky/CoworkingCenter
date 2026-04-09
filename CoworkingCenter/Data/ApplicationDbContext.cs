// Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using CoworkingCenter.Models;

namespace CoworkingCenter.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Resident> Residents { get; set; }
        public DbSet<Workplace> Workplaces { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<MeetingRoom> MeetingRooms { get; set; }
        public DbSet<Payment> Payments { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Resident)
                .WithMany(r => r.Bookings)
                .HasForeignKey(b => b.ResidentId)
                .OnDelete(DeleteBehavior.Restrict);
                
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Workplace)
                .WithMany(w => w.Bookings)
                .HasForeignKey(b => b.WorkplaceId)
                .OnDelete(DeleteBehavior.Restrict);
                
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Resident)
                .WithMany(r => r.Payments)
                .HasForeignKey(p => p.ResidentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}