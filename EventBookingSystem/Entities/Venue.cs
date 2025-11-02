using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBookingSystem.Entities
{
    public class Venue
    {
        public int VenueId { get; set; }
        public int Capacity { get; set; }

        //The list of Events held at the venue, not referenced in the DB but we can load them as needed for convenience (same as User.Bookings)
        public List<Event> Events { get; set; } = new List<Event>();

    }
}