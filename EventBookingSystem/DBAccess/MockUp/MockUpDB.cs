using EventBookingSystem.Entities;
using EventBookingSystem.EventSeating;
using System.Collections.Generic;

namespace EventBookingSystem.DBAccess.MockUp
{
    // This class simulates a database with in-memory collections for testing purposes
    public class MockUpDB
    {
        public List<User> Users { get; set; }
        public List<Event> Events { get; set; }
        public List<Booking> Bookings { get; set; }
        public List<Venue> Venues { get; set; }

        public MockUpDB()
        {
            // Initialize collections
            Users = new List<User>();
            Events = new List<Event>();
            Bookings = new List<Booking>();
            Venues = new List<Venue>();
            //Create the seating types
            var openAirStrategy = new OpenAirSeatType();
            var reservedStrategy = new ReservedSeatType();
            var sectionedStrategy = new SectionedSeatType();
            //Create venues
            var venue1 = new Venue { VenueId = 1, Capacity = 1000 };
            var venue2 = new Venue { VenueId = 2, Capacity = 500 }; 
            Venues.AddRange(new[] { venue1, venue2 });
            //Create Users
            var user1 = new User { UserId = 1 };
            var user2 = new User { UserId = 2 };
            Users.AddRange(new[] { user1, user2 });
            //Create Events
            var event1 = new Event(openAirStrategy, venue1) { EventId = 1 };
            var event2 = new Event(reservedStrategy, venue2) { EventId = 2 };
            var event3 = new Event(sectionedStrategy, venue1) { EventId = 3 };
            Events.AddRange(new[] { event1, event2, event3 });
            //Create Bookings
            var booking1 = new Booking(user1, event1) { BookingId = 1 };
            var booking2 = new Booking(user2, event2) { BookingId = 2 };
            var booking3 = new Booking(user1, event3) { BookingId = 3 };
            Bookings.AddRange(new[] { booking1, booking2, booking3 });
        }
    }
}