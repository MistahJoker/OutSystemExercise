using EventBookingSystem.Entities;

namespace EventBookingSystem.DBAccess.Interfaces
{
    public interface IVenueRepository
    {
        List<Venue> GetAllVenues();
        Venue? GetVenueById(int venueId);
        Venue AddVenue(Venue venue);
        void UpdateVenue(Venue venue);
        void DeleteVenue(int venueId);
    }
}