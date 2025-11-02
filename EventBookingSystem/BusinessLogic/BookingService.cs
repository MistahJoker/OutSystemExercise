using EventBookingSystem.DBAccess.Interfaces;
using EventBookingSystem.Entities;

namespace EventBookingSystem.Services
{
    public class BookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IVenueRepository _venueRepository;


        public BookingService(
            IUserRepository userRepository,
            IEventRepository eventRepository,
            IVenueRepository venueRepository,
            IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
            _userRepository = userRepository;
            _eventRepository = eventRepository;
            _venueRepository = venueRepository;
        }
        public Booking CreateBooking(int userId, int eventId)
        {
            var user = _userRepository.GetUserById(userId);
            if (user == null)
            {
                throw new ArgumentException("Invalid user ID");
            }
            var evnt = _eventRepository.GetEventById(eventId);
            if (evnt == null)
            {
                throw new ArgumentException("Invalid event ID");
            }
            int numberOfBookingsForEvent = _bookingRepository.GetBookingsByEventId(eventId)?.Count ?? 0;
            if (numberOfBookingsForEvent >= evnt.Venue?.Capacity)
            {
                throw new InvalidOperationException("No available seats for this event");
            }
            var booking = new Booking(user, evnt);
            return _bookingRepository.AddBooking(booking);
        }
    }
}