using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBookingSystem.Entities
{
    public class User
    {
        public int UserId { get; set; }
        
        //The list of bookings made by the user, not referenced in the DB but we can load them as needed for convenience
        public List<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
