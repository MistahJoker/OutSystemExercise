using EventBookingSystem.DBAccess.Interfaces;
using EventBookingSystem.Entities;
using EventBookingSystem.ExternalAPIs;

namespace EventBookingSystem.Services
{
    public class BookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IVenueRepository _venueRepository;
        private readonly IPaymentGeteway _paymentGateway;


        public BookingService(
            IUserRepository userRepository,
            IEventRepository eventRepository,
            IVenueRepository venueRepository,
            IBookingRepository bookingRepository,
            IPaymentGeteway paymentGateway)
        {
            _bookingRepository = bookingRepository;
            _userRepository = userRepository;
            _eventRepository = eventRepository;
            _venueRepository = venueRepository;
            _paymentGateway = paymentGateway;
        }
        public Booking CreateBooking(int userId, int eventId,string creditCardNumber)
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
            var venue = _venueRepository.GetVenueById(evnt.VenueId);
            if (venue == null)
            {
                throw new ArgumentException("Invalid venue ID");
            }
            int numberOfBookingsForEvent = _bookingRepository.GetBookingsByEventId(eventId).Count;
            if (numberOfBookingsForEvent >= evnt.Venue?.Capacity)
            {
                throw new InvalidOperationException("No available seats for this event");
            }
            var paymentResult = _paymentGateway.ProcessPayment(creditCardNumber);
            if (paymentResult == null || !paymentResult.IsSuccessful)
            {
                throw new InvalidOperationException("Payment failed or was rejected.");
            }
            var booking = new Booking(user, evnt)
            {
                PaymentId = paymentResult.PaymentId,
                PaymentStatus=PaymentStatus.Paid
            };
            return _bookingRepository.AddBooking(booking);
        }
    }
}