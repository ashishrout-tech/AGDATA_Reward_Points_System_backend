using Project.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities.Event
{
    public class EventMetadata
    {
        public User Organizer { get; private set; }
        public List<string> Tags { get; private set; } = new();

        public EventMetadata(User organizer)
        {
            Organizer = organizer;
        }

        public void AddTag(string tag)
        {
            if (!Tags.Contains(tag))
                Tags.Add(tag);
        }
    }
}
