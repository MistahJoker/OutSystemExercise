using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBookingSystem.Entities
{
    public class Booking
    {
        public int BookingId { get; set; }
        public string? PaymentId { get; set; }
        //Enum
        public PaymentStatus PaymentStatus { get; set; }
        //Foreign Key
        public int UserId { get; set; }
        //Direct reference to the User entity
        public User? User { get; set; }
        //Foreign Key
        public int EventId { get; set; }
        //Direct reference to the Event entity
        public Event? Event { get; set; }

        private Booking() { }

        public Booking(User user, Event evnt)
        {
            User = user;
            UserId = user.UserId;
            Event = evnt;
            EventId = evnt.EventId;
        }
        public override string ToString()
        {
            return $"Booking {BookingId} for User {User?.UserId} at Event {Event?.EventId} and Payment Status {PaymentStatus}";
        }
    }
}