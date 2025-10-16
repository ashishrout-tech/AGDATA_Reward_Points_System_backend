using Project.Domain.Entities;
using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services.Implementation
{
    public class RedemptionService
    {
        private readonly List<Redemption> _redemptions = new();

        public Redemption CreateRedemption(Guid userId, Guid productId)
        {
            // add redemption checks
            var redemption = new Redemption(userId, productId, RedemptionStatus.Pending);
            _redemptions.Add(redemption);
            return redemption;
        }

        public void ApproveRedemption(Guid redemptionId)
        {
            var redemption = _redemptions.FirstOrDefault(r => r.Id == redemptionId)
                ?? throw new InvalidOperationException("Redemption not found.");

            redemption.MarkCompleted();
        }

        public void RejectRedemption(Guid redemptionId)
        {
            var redemption = _redemptions.FirstOrDefault(r => r.Id == redemptionId)
                ?? throw new InvalidOperationException("Redemption not found.");

            redemption.MarkFailed();
        }

        public IEnumerable<Redemption> GetRedemptionsByUser(Guid userId)
        {
            return _redemptions.Where(r => r.UserId == userId).ToList();
        }

        public IEnumerable<Redemption> GetAllRedemptions() => _redemptions.AsReadOnly();
    }
}
