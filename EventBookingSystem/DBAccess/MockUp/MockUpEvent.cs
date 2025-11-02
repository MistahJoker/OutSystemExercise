using EventBookingSystem.DBAccess.Interfaces;
using EventBookingSystem.Entities;
using EventBookingSystem.EventSeating;

namespace EventBookingSystem.DBAccess.MockUp
{
    public class MockUpEvent : IEventRepository
    {
        private readonly MockUpDB _db;

        // Constructor to initialize with some mock data
        public MockUpEvent(MockUpDB db)
        {
            _db = db;
        }

        public List<Event> GetAllEvents()
        {
            return _db.Events;
        }
        
        public List<Event> GetFutureEventsWithAvailability()
        {
            return _db.Events.Where(e => e.Date>DateTime.Now).ToList();
        }
        public Event? GetEventById(int eventId)
        {
            if (eventId <= 0)
            {
                throw new ArgumentException("Event ID must be greater than 0.", nameof(eventId));
            }
            return _db.Events.FirstOrDefault(e => e.EventId == eventId);
        }

        public Event AddEvent(Event evnt)
        {
            if (evnt == null)
            {
                throw new ArgumentNullException(nameof(evnt));
            }
            //simulate auto-incrementing primary key
            int newEventId = _db.Events.Any() ? _db.Events.Max(e => e.EventId) + 1 : 1;
            evnt.EventId = newEventId;
            _db.Events.Add(evnt);
            return evnt;
        }

        public void UpdateEvent(Event evnt)
        {
            if (evnt == null)
            {
                throw new ArgumentNullException(nameof(evnt));
            }
            var existingEvent = _db.Events.FirstOrDefault(e => e.EventId == evnt.EventId);
            if (existingEvent == null)
            {
                throw new ArgumentException($"Event with EventId {evnt.EventId} does not exist.");
            }
            // upadte other properties as needed
            existingEvent.Venue = evnt.Venue;
            existingEvent.VenueId = evnt.VenueId;
        }

        public void DeleteEvent(int eventId)
        {
            if (eventId <= 0)
            {
                throw new ArgumentException(nameof(eventId));
            }
            var evnt = _db.Events.FirstOrDefault(e => e.EventId == eventId);
            if (evnt == null)
            {
                throw new ArgumentException($"Event with EventId {eventId} does not exist.");
            }
            _db.Events.Remove(evnt);
        }
    }
}