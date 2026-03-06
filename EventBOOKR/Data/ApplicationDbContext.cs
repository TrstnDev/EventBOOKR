using Microsoft.EntityFrameworkCore;
using EventBOOKR.Models;

namespace EventBOOKR.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Venue> Venues { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<EventSchedule> EventSchedules { get; set; }
    public DbSet<Booking> Bookings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // 1. Link EventSchedule to Venue
        modelBuilder.Entity<EventSchedule>()
            .HasOne(es => es.Venue)
            .WithMany(v => v.Schedules)
            .HasForeignKey(es => es.VenueId)
            .OnDelete(DeleteBehavior.Restrict);

        // 2. Link EventSchedule to Event
        modelBuilder.Entity<EventSchedule>()
            .HasOne(es => es.Event)
            .WithMany(e => e.Schedules)
            .HasForeignKey(es => es.EventId)
            .OnDelete(DeleteBehavior.Restrict);
        
        // 3. Link Booking to EventSchedule
        modelBuilder.Entity<Booking>()
            .HasOne(b => b.Schedule)
            .WithMany(s => s.Bookings)
            .HasForeignKey(b => b.ScheduleId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}