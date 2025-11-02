using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventBookingSystem.EventSeating;

namespace EventBookingSystem.Entities
{
    public class Event
    {
        //Need the private set to make the property read-only and only able to set on construction
        public ISeatingType? SeatingType { get; private set; }
        public int EventId { get; set; }
        // Foreign Key
        public int VenueId { get; set; }
        //Direct reference to the Venue entity
        public Venue? Venue { get; set; }
        //List of bookings made for this event, not referenced in the DB but we can load them as needed for convenience
        public List<Booking> Bookings { get; set; } = new List<Booking>();
        public DateTime Date { get; set; }
        //Needed for the Entity Framework
        private Event() { }

        public Event(ISeatingType seatingType, Venue venue)
        {
            SeatingType = seatingType;
            Venue = venue;
            VenueId = venue.VenueId;
        }
        public void ManageSeating()
        {
            SeatingType?.ManageSeating();
        }
        public override string ToString()
        {
            return $"Event {EventId} at {Venue?.VenueId} on {Date.ToShortDateString()}";
        }
    }
}