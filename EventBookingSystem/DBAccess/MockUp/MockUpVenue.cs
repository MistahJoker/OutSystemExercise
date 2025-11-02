using EventBookingSystem.DBAccess.Interfaces;
using EventBookingSystem.Entities;

namespace EventBookingSystem.DBAccess.MockUp
{
    public class MockUpVenue : IVenueRepository
    {
        private readonly MockUpDB _db;

        // Constructor to initialize with some mock data
        public MockUpVenue(MockUpDB db)
        {
            _db = db;
        }
        public List<Venue> GetAllVenues()
        {
            return _db.Venues;
        }
        public Venue? GetVenueById(int venueId)
        {
            if (venueId<=0)
            {
                throw new ArgumentException(nameof(venueId));
            }
            return _db.Venues.FirstOrDefault(v => v.VenueId == venueId);
        }
        public Venue AddVenue(Venue venue)
        {
            if (venue == null)
            {
                throw new ArgumentNullException(nameof(venue));
            }
            //simulate auto-incrementing primary key
            int newVenueId = _db.Venues.Any() ? _db.Venues.Max(v => v.VenueId) + 1 : 1;
            venue.VenueId = newVenueId;
            _db.Venues.Add(venue);
            return venue;
        }
        public void UpdateVenue(Venue venue)
        {
            if (venue == null)
            {
                throw new ArgumentNullException(nameof(venue));
            }
            var existingVenue = _db.Venues.FirstOrDefault(v=>v.VenueId==venue.VenueId);
            if (existingVenue == null)
            {
                throw new ArgumentException($"Venue with VenueId {venue.VenueId} does not exist.");
            }
            
            existingVenue.Capacity = venue.Capacity;
        }
        public void DeleteVenue(int venueId)
        {
            if (venueId > 0)
            {
                throw new ArgumentException(nameof(venueId));
            }
            var venue = _db.Venues.FirstOrDefault(v=>v.VenueId==venueId);
            if (venue == null)
            {
                throw new ArgumentException($"Venue with VenueId {venueId} does not exist.");
            }

            _db.Venues.Remove(venue);
        }
    }
}