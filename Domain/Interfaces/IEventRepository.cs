using Project.Domain.Entities.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Interfaces
{
    public interface IEventRepository
    {
        Event Add(Event e);
        Event? GetById(Guid id);
        List<Event> GetAll();
    }
}
