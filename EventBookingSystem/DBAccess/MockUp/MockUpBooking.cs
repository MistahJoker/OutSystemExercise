using System.Linq.Expressions;
using EventBookingSystem.DBAccess.Interfaces;
using EventBookingSystem.Entities;
using EventBookingSystem.EventSeating;

namespace EventBookingSystem.DBAccess.MockUp
{
    public class MockUpBooking : IBookingRepository
    {
        private readonly MockUpDB _db;
        public MockUpBooking(MockUpDB db)
        {
            _db = db;
        }

        public List<Booking> GetAllBookings()
        {
            return _db.Bookings;
        }

        public Booking? GetBookingById(int bookingId)
        {
            if (bookingId<=0)
            {
                throw new ArgumentException(nameof(bookingId));
            }
            return _db.Bookings.FirstOrDefault(b => b.BookingId == bookingId);
        }

        public List<Booking> GetBookingsByEventId(int eventId)
        {
            if (eventId <= 0)
            {
                throw new ArgumentException(nameof(eventId));
            }
            
            return _db.Bookings.Where(b => b.Event!=null && b.EventId == eventId).ToList();
        }

        public Booking AddBooking(Booking booking)
        {
            if (booking == null)
            {
                throw new ArgumentNullException(nameof(booking));
            }
            //simulate auto-incrementing primary key
            int newBookingId = _db.Bookings.Any() ? _db.Bookings.Max(b => b.BookingId) + 1 : 1;
            booking.BookingId = newBookingId;
            _db.Bookings.Add(booking);
            return booking;
        }

        public void UpdateBooking(Booking booking)
        {
            if (booking == null)
            {
                throw new ArgumentNullException(nameof(booking));
            }
            var existingBooking = _db.Bookings.FirstOrDefault(b => b.BookingId == booking.BookingId);
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
            var booking = _db.Bookings.FirstOrDefault(b => b.BookingId == bookingId);
            if (booking == null)
            {
                throw new ArgumentException($"Booking with BookingId {bookingId} does not exist.");
            }
            _db.Bookings.Remove(booking);
        }
        public List<Booking> FindBookingsForPaidUsersAtVenue(int venueId)
        {
            if (venueId <= 0)
            {
                throw new ArgumentException(nameof(venueId));
            }
            var eventIdsForVenue = _db.Events
                                .Where(e => e.VenueId == venueId)
                                .Select(e => e.EventId)
                                .ToList();
            var allBookingAtVenue = _db.Bookings
                                .Where(b => eventIdsForVenue.Contains(b.EventId))
                                .ToList();
            var paidUserIds = allBookingAtVenue
                                .Where(b => b.PaymentStatus == PaymentStatus.Paid)
                                .Select(u => u.UserId)
                                .Distinct()
                                .ToList();
            var bookings = allBookingAtVenue
                        .Where(b => paidUserIds.Contains(b.UserId))
                        .ToList();
            return bookings;
        }
        public List<Booking> GetBookingsByUserId(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException(nameof(userId));
            }
            return _db.Bookings.Where(b =>b.UserId == userId).ToList();
        }
        public List<Booking> GetBookingsByVenueId(int venueId)
        { 
            if (venueId <= 0)
            {
                throw new ArgumentException(nameof(venueId));
            }
            var eventIdsForVenue = _db.Events
                                .Where(e => e.VenueId == venueId)
                                .Select(e => e.EventId)
                                .ToList();
            var bookings = _db.Bookings
                                .Where(b => eventIdsForVenue.Contains(b.EventId))
                                .ToList();
            return bookings;
        }
    

    }
}