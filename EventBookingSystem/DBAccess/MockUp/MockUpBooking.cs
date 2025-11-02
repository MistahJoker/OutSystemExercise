using EventBookingSystem.DBAccess.Interfaces;
using EventBookingSystem.Entities;
using EventBookingSystem.EventSeating;

namespace EventBookingSystem.DBAccess.MockUp
{
    public class MockUpBooking : IBookingRepository
    {
        private readonly List<Booking> _bookings;

        // Constructor to initialize with some mock data
        public MockUpBooking()
        {
            //Just to help create the bookings
            User _user1 = new User { UserId = 1 };
            User _user2 = new User { UserId = 2 };  
            Venue _venue1 = new Venue { VenueId = 1 };
            Venue _venue2 = new Venue { VenueId = 2 };
            Event _event1 = new Event(new OpenAirSeatType(), _venue1) { EventId = 1 };
            Event _event2 = new Event(new ReservedSeatType(), _venue2) { EventId = 2 };

            _bookings = new List<Booking>
            {
                new Booking(_user1, _event1) { BookingId = 1, PaymentStatus = PaymentStatus.Paid },
                new Booking(_user2, _event2) { BookingId = 2, PaymentStatus = PaymentStatus.Pending },
                new Booking(_user1, _event2) { BookingId = 3, PaymentStatus = PaymentStatus.Failed },
                new Booking(_user2, _event1) { BookingId = 4, PaymentStatus = PaymentStatus.Refunded }
            };
        }

        public List<Booking> GetAllBookings()
        {
            return _bookings;
        }

        public Booking? GetBookingById(int bookingId)
        {
            if (bookingId<=0)
            {
                throw new ArgumentException(nameof(bookingId));
            }
            return _bookings.FirstOrDefault(b => b.BookingId == bookingId);
        }

        public List<Booking>? GetBookingsByEventId(int eventId)
        {
            if (eventId<=0)
            {
                throw new ArgumentException(nameof(eventId));
            }
            return _bookings.Any() ? _bookings.Where (b => b.Event != null && b.Event.EventId == eventId).ToList() : null;
        }

        public Booking AddBooking(Booking booking)
        {
            if (booking == null)
            {
                throw new ArgumentNullException(nameof(booking));
            }
            //simulate auto-incrementing primary key
            int newBookingId = _bookings.Any() ? _bookings.Max(b => b.BookingId) + 1 : 1;
            booking.BookingId = newBookingId;
            _bookings.Add(booking);
            return booking;
        }

        public void UpdateBooking(Booking booking)
        {
            if (booking == null)
            {
                throw new ArgumentNullException(nameof(booking));
            }
            var existingBooking = _bookings.FirstOrDefault(b => b.BookingId == booking.BookingId);
            if (existingBooking == null)
            {
                throw new ArgumentException($"Booking with BookingId {booking.BookingId} does not exist.");
            }
            existingBooking.PaymentStatus = booking.PaymentStatus;
            existingBooking.PaymentId = booking.PaymentId;
        }

        public void DeleteBooking(int bookingId)
        {
            if (bookingId <= 0)
            {
                throw new ArgumentException(nameof(bookingId));
            }
            var booking = _bookings.FirstOrDefault(b => b.BookingId == bookingId);
            if (booking == null)
            {
                throw new ArgumentException($"Booking with BookingId {bookingId} does not exist.");
            }
            _bookings.Remove(booking);
        }
    }
}