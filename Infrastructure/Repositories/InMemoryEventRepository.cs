using Project.Domain.Entities.Event;
using Project.Domain.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.Repositories
{
    public class InMemoryEventRepository : IEventRepository
    {
        private readonly ConcurrentDictionary<Guid, Event> _store = new ConcurrentDictionary<Guid, Event>();

        public Event Add(Event e)
        {
            _store[e.Id] = e;
            return e;
        }

        public Event? GetById(Guid id) => _store.TryGetValue(id, out var e) ? e : null;

        public List<Event> GetAll() => _store.Values.ToList();
    }
}
