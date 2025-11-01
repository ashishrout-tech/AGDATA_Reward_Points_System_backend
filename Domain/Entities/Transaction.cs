using Project.Domain.Enums;
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
            if (userId == Guid.Empty)
                throw new ArgumentException("UserId cannot be empty.", nameof(userId));
            if (!Enum.IsDefined(typeof(TransactionType), type))
                throw new ArgumentException("Invalid transaction type.", nameof(type));
            if (points <= 0)
                throw new ArgumentOutOfRangeException(nameof(points), "Points must be greater than zero.");
            if (string.IsNullOrWhiteSpace(details))
                throw new ArgumentException("Details cannot be null or whitespace.", nameof(details));

            Id = Guid.NewGuid();
            UserId = userId;
            Type = type;
            Points = points;
            Details = details;
            TimeStamp = DateTime.UtcNow;
        }
    }
}
