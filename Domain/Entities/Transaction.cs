using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities
{
    public class Transaction
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public TransactionType Type { get; private set;  }
        public int Points { get; private set; }
        public string Details { get; private set; }
        public DateTime TimeStamp { get; private set; }

        public Transaction(Guid userId, TransactionType type, int points, string details)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Type = type;
            Points = points;
            Details = details;
            TimeStamp = DateTime.UtcNow;
        }
    }
}
