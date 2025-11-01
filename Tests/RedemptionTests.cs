using Project.Domain.Enums;
using Project.Services.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Project.Tests
{
    public class RedemptionServiceTests
    {
        [Fact]
        public void CreateRedemption_ShouldAddPendingRedemption()
        {
            var service = new RedemptionService();
            var userId = Guid.NewGuid();
            var productId = Guid.NewGuid();

            var redemption = service.CreateRedemption(userId, productId);

            Assert.NotNull(redemption);
            Assert.Equal(userId, redemption.UserId);
            Assert.Equal(productId, redemption.ProductId);
            Assert.Equal(RedemptionStatus.Pending, redemption.Status);
        }

        [Fact]
        public void ApproveRedemption_ShouldChangeStatusToApproved()
        {
            var service = new RedemptionService();
            var redemption = service.CreateRedemption(Guid.NewGuid(), Guid.NewGuid());

            service.ApproveRedemption(redemption.Id);

            Assert.Equal(RedemptionStatus.Approved, redemption.Status);
        }

        [Fact]
        public void RejectRedemption_ShouldChangeStatusToRejected()
        {
            var service = new RedemptionService();
            var redemption = service.CreateRedemption(Guid.NewGuid(), Guid.NewGuid());

            service.RejectRedemption(redemption.Id);

            Assert.Equal(RedemptionStatus.Rejected, redemption.Status);
        }

        [Fact]
        public void GetRedemptionsByUser_ShouldReturnOnlyUserRedemptions()
        {
            var service = new RedemptionService();
            var user1 = Guid.NewGuid();
            var user2 = Guid.NewGuid();

            var r1 = service.CreateRedemption(user1, Guid.NewGuid());
            var r2 = service.CreateRedemption(user1, Guid.NewGuid());
            var r3 = service.CreateRedemption(user2, Guid.NewGuid());

            var user1Redemptions = service.GetRedemptionsByUser(user1).ToList();
            Assert.Equal(2, user1Redemptions.Count);
            Assert.All(user1Redemptions, r => Assert.Equal(user1, r.UserId));

            var user2Redemptions = service.GetRedemptionsByUser(user2).ToList();
            Assert.Single(user2Redemptions);
            Assert.Equal(user2, user2Redemptions[0].UserId);
        }

        [Fact]
        public void GetAllRedemptions_ShouldReturnAllRedemptions()
        {
            var service = new RedemptionService();
            service.CreateRedemption(Guid.NewGuid(), Guid.NewGuid());
            service.CreateRedemption(Guid.NewGuid(), Guid.NewGuid());

            var allRedemptions = service.GetAllRedemptions().ToList();
            Assert.Equal(2, allRedemptions.Count);
        }

        [Fact]
        public void ApproveRedemption_NonExistentId_ShouldThrow()
        {
            var service = new RedemptionService();
            var invalidId = Guid.NewGuid();

            var ex = Assert.Throws<InvalidOperationException>(() => service.ApproveRedemption(invalidId));
            Assert.Equal("Redemption not found.", ex.Message);
        }

        [Fact]
        public void RejectRedemption_NonExistentId_ShouldThrow()
        {
            var service = new RedemptionService();
            var invalidId = Guid.NewGuid();

            var ex = Assert.Throws<InvalidOperationException>(() => service.RejectRedemption(invalidId));
            Assert.Equal("Redemption not found.", ex.Message);
        }
    }
}
