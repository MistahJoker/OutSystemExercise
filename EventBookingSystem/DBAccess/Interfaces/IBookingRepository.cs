using EventBookingSystem.Entities;

namespace EventBookingSystem.DBAccess.Interfaces
{
    public interface IBookingRepository
    {
        List<Booking> GetAllBookings();
        Booking? GetBookingById(int bookingId);
        List<Booking>? GetBookingsByEventId(int eventId);
        Booking AddBooking(Booking booking);
        void UpdateBooking(Booking booking);
        void DeleteBooking(int bookingId);
    }
}