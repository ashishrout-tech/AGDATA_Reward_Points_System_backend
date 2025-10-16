using Project.Domain.Enums;
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
        public RedemptionStatus Status { get; set; }
        public DateTime Timestamp { get; set; }

        public Redemption(Guid userId, Guid productId, RedemptionStatus status)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            ProductId = productId;
            Status = status;
            Timestamp = DateTime.UtcNow;
        }

        internal void MarkCompleted()
        {
            Status = RedemptionStatus.Approved;
        }

        internal void MarkFailed()
        {
            Status = RedemptionStatus.Rejected;
        }
    }
}
