using EventBookingSystem.DBAccess.Interfaces;
using EventBookingSystem.Entities;
using EventBookingSystem.EventSeating;

namespace EventBookingSystem.DBAccess.MockUp
{
    public class MockUpEvent : IEventRepository
    {
        private readonly List<Event> _events;

        // Constructor to initialize with some mock data
        public MockUpEvent()
        {
            //Just to help create the Events 
            Venue _venue1 = new Venue { VenueId = 1 };
            Venue _venue2 = new Venue { VenueId = 2 };

            _events = new List<Event>
            {
                new Event(new OpenAirSeatType(), _venue1) { EventId = 1},
                new Event(new ReservedSeatType(), _venue2) { EventId = 2 },
                new Event(new SectionedSeatType(), _venue1) { EventId = 3 }
            };
        }

        public List<Event> GetAllEvents()
        {
            return _events;
        }

        public Event? GetEventById(int eventId)
        {
            if (eventId <=0)
            {
                throw new ArgumentException(nameof(eventId));
            }
            return _events.FirstOrDefault(e => e.EventId == eventId);
        }

        public Event AddEvent(Event evnt)
        {
            if (evnt == null)
            {
                throw new ArgumentNullException(nameof(evnt));
            }
            //simulate auto-incrementing primary key
            int newEventId = _events.Any() ? _events.Max(e => e.EventId) + 1 : 1;
            evnt.EventId = newEventId;
            _events.Add(evnt);
            return evnt;
        }

        public void UpdateEvent(Event evnt)
        {
            if (evnt == null)
            {
                throw new ArgumentNullException(nameof(evnt));
            }
            var existingEvent = _events.FirstOrDefault(e => e.EventId == evnt.EventId);
            if (existingEvent == null)
            {
                throw new ArgumentException($"Event with EventId {evnt.EventId} does not exist.");
            }
            // upadte other properties as needed
        }

        public void DeleteEvent(int eventId)
        {
            var evnt = _events.FirstOrDefault(e => e.EventId == eventId);
            if (evnt == null)
            {
                throw new ArgumentException($"Event with EventId {eventId} does not exist.");
            }
            _events.Remove(evnt);
        }
    }
}