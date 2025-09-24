using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities
{
    public class Event
    {
        public Guid Id {  get; set; }
        public string EventName { get; set; }
        public string Description { get; set; }
        public DateTime? Date {  get; set; }

        public Event(string eventName, string description, DateTime? date)
        {
            EventName = eventName;
            Description = description;
            Date = date;
        }
    }
}
