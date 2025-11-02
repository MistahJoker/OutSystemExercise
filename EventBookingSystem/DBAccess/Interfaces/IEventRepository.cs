using EventBookingSystem.Entities;

namespace EventBookingSystem.DBAccess.Interfaces
{
    public interface IEventRepository
    {
        List<Event> GetAllEvents();
        List<Event> GetFutureEventsWithAvailability();
        Event? GetEventById(int eventId);
        Event AddEvent(Event evnt);
        void UpdateEvent(Event evnt);
        void DeleteEvent(int eventId);
    }
}