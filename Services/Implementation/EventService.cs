using Project.Domain.Entities.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services.Implementation
{
    public class EventService
    {
        private readonly List<Event> _events = new();

        public Event CreateEvent(string title, string description, EventMetadata metadata, EventSchedule schedule)
        {
            var ev = new Event(title, description, metadata, schedule);
            _events.Add(ev);
            return ev;
        }

        public void RegisterParticipant(Guid eventId, EventParticipant participant)
        {
            var ev = _events.FirstOrDefault(e => e.Id == eventId)
                ?? throw new InvalidOperationException("Event not found.");

            ev.AddParticipant(participant);
        }

        public void RemoveParticipant(Guid eventId, Guid userId)
        {
            var ev = _events.FirstOrDefault(e => e.Id == eventId)
                ?? throw new InvalidOperationException("Event not found.");
            ev.RemoveParticipant(userId);
        }

        public void CancelEvent(Guid eventId)
        {
            var ev = _events.FirstOrDefault(e => e.Id == eventId)
                ?? throw new InvalidOperationException("Event not found.");

            ev.CancelEvent();
        }

        public IEnumerable<Event> GetAllEvents() => _events.AsReadOnly();

        public Event? GetEventById(Guid eventId) => _events.FirstOrDefault(e => e.Id == eventId);
    }
}
