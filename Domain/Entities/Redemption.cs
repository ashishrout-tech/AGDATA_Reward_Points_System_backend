using Project.Domain.Enums;
using Project.Domain.Entities.Users;
using Project.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities
{
    public class Redemption
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public User User { get; set; } = null!;
        public Product.Product Product { get; set; } = null!;
        public RedemptionStatus Status { get; set; }
        public DateTime Timestamp { get; set; }
        public Redemption() { }

        public Redemption(Guid userId, Guid productId, RedemptionStatus status)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("UserId cannot be empty.", nameof(userId));
            if (productId == Guid.Empty)
                throw new ArgumentException("ProductId cannot be empty.", nameof(productId));
            if (!Enum.IsDefined(typeof(RedemptionStatus), status))
                throw new ArgumentOutOfRangeException(nameof(status), "Invalid redemption status.");

            Id = Guid.NewGuid();
            UserId = userId;
            ProductId = productId;
            Status = status;
            Timestamp = DateTime.UtcNow;
        }

        internal void MarkCompleted()
        {
            if (Status != RedemptionStatus.Pending)
                throw new InvalidOperationException("Redemption can only be completed from Pending status.");
            Status = RedemptionStatus.Approved;
        }

        internal void MarkFailed()
        {
            if (Status != RedemptionStatus.Pending)
                throw new InvalidOperationException("Redemption can only be failed from Pending status.");
            Status = RedemptionStatus.Rejected;
        }
    }
}
