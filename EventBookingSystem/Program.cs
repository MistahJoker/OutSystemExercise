using EventBookingSystem.DBAccess.Interfaces;
using EventBookingSystem.DBAccess.MockUp;
using EventBookingSystem.Entities;
using EventBookingSystem.EventSeating;
using EventBookingSystem.ExternalAPIs;
using EventBookingSystem.Services;
using System.Collections.Generic;
using System;

Console.WriteLine("======================================");
Console.WriteLine("🚀 Event Booking System booting up... 🚀");
Console.WriteLine("======================================");

//Initialze db
var db = new MockUpDB();
Console.WriteLine($"DB created with {db.Users.Count} users, {db.Events.Count} events, and {db.Bookings.Count} bookings.");

//Initialize repositories
IUserRepository userRepository = new MockUpUser(db);
IEventRepository eventRepository = new MockUpEvent(db);
IVenueRepository venueRepository = new MockUpVenue(db);
IBookingRepository bookingRepository = new MockUpBooking(db);
IPaymentGeteway paymentGateway = new PaymentGetway();

//Initialize services
BookingService bookingService = new BookingService(userRepository, eventRepository, venueRepository, bookingRepository, paymentGateway);

Console.WriteLine("Setup complete. Running tests now...");

//Update some of the mock data
db.Bookings[0].PaymentStatus = PaymentStatus.Paid;
db.Bookings[1].PaymentStatus = PaymentStatus.Paid;
db.Bookings[2].PaymentStatus = PaymentStatus.Pending;

Console.WriteLine("\n======================================");
Console.WriteLine("Test 1: Core booking and capacity logic");
Console.WriteLine("======================================");
try
{
    Console.WriteLine("Trying to create a booking for User 2 at Event 1");
    Booking booking = bookingService.CreateBooking(2, 1, "123456789");
    Console.WriteLine($"Booking created with ID: {booking.BookingId}");

}
catch (Exception ex)
{
    Console.WriteLine($"Booking creation failed: {ex.Message}");
}
try
{
    Venue? venue2 = venueRepository.GetVenueById(2);
    venue2?.Capacity = 1;
    Console.WriteLine("Updating venue capacity to 1");
    Console.WriteLine("Trying to book for User 1 at Event 2 (should fail as it already has a booking)");
    Booking booking = bookingService.CreateBooking(1, 2, "123456789");
    Console.WriteLine("Failed Booking was created");

}
catch (Exception ex)
{
    Console.WriteLine($"Success failed as expected: {ex.Message}");
}

Console.WriteLine("\n======================================");
Console.WriteLine("Test 2: Standard Retrival Methods");
Console.WriteLine("======================================");

Console.WriteLine("--- GetBookingByVenueId(1) ---");
List<Booking> user1Bookings = bookingRepository.GetBookingsByVenueId(1);
foreach (var booking in user1Bookings)
{
    Console.WriteLine(booking.ToString());
}

Console.WriteLine("--- GetBookingsByVenueId(1) ---");
List<Booking> venue1Bookings = bookingRepository.GetBookingsByVenueId(1);
foreach (var booking in venue1Bookings)
{
    Console.WriteLine(booking.ToString());
}

Console.WriteLine("--- GetFutureEventsWithAvailability(1) ---");
List<Event> futureEvents = eventRepository.GetFutureEventsWithAvailability();
foreach (var evnt in futureEvents)
{
    Console.WriteLine(evnt.ToString());
}

Console.WriteLine("\n======================================");
Console.WriteLine("Test 3: Advanced SQL-like querries");
Console.WriteLine("======================================");

List<Booking> paidUserBookings= bookingRepository.FindBookingsForPaidUsersAtVenue(1);
foreach (var booking in paidUserBookings)
{
    Console.WriteLine(booking.ToString());
}

List<User> usersWithoutBookings = userRepository.FindUsersWithoutBookingsInVenue(2);
foreach (var user in usersWithoutBookings)
{
    Console.WriteLine(user.ToString());
}

Console.WriteLine("\n======================================");
Console.WriteLine("All tests completed");
Console.WriteLine("======================================");