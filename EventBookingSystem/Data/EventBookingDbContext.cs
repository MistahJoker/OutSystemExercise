using Microsoft.EntityFrameworkCore;

namespace EventBookingSystem.Entities;

public class EventBookingDbContext : DbContext
{
    // Classes to create tables for
    public DbSet<User> Users { get; set; }
    public DbSet<Venue> Venues { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Booking> Bookings { get; set; }

    // This is some boilerplate to tell it where to find the db file
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // For this to work, it just needs a simple connection string
        options.UseSqlite("Data Source=events.db");
    }

    // This part is optional but good practice.
    // It explicitly tells EF Core about your relationships.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>()
            .HasOne(b => b.User)          // A Booking has one User
            .WithMany(u => u.Bookings)    // A User has many Bookings
            .HasForeignKey(b => b.UserId); // The link is the UserId

        modelBuilder.Entity<Booking>()
            .HasOne(b => b.Event)         // A Booking has one Event
            .WithMany(e => e.Bookings)    // An Event has many Bookings
            .HasForeignKey(b => b.EventId); // The link is the EventId

        modelBuilder.Entity<Event>()
            .HasOne(e => e.Venue)         // An Event has one Venue
            .WithMany(v => v.Events)      // A Venue has many Events
            .HasForeignKey(e => e.VenueId); // The link is the VenueId

        
        // This tells EF Core to ignore the ISeatingStrategy
        // property, since it can't be stored in a database column.
        modelBuilder.Entity<Event>()
            .Ignore(e => e.SeatingType);
        
        // This tells EF Core how to save your enum
        modelBuilder.Entity<Booking>()
            .Property(b => b.PaymentStatus)
            .HasConversion<string>(); // Saves the enum as a string
    }
}