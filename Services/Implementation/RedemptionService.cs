using Project.Domain.Entities;
using Project.Domain.Enums;
using Project.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace Project.Services.Implementation
{
    public class RedemptionService
    {
        private readonly List<Redemption> _redemptions = new();
        private readonly IUserAccountRepository _usersAccount;
        private readonly IProductPriceRepository _productsPoint;

        public RedemptionService(IUserAccountRepository usersAccount, IProductPriceRepository productsPoint)
        {
            _usersAccount = usersAccount;
            _productsPoint = productsPoint;
        }

        public Redemption CreateRedemption(Guid userId, Guid productId)
        {
            var userAccount = _usersAccount.GetAccountByUserId(userId);
            var userPoints = userAccount.Points;
            var productPoints = _productsPoint.GetByProductId(productId);
            var requiredPoints = productPoints.CurrentPoints;
            if (requiredPoints > userPoints) throw new InvalidOperationException("Not enough points");
            userAccount.RedeemPoints((int)requiredPoints);
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
