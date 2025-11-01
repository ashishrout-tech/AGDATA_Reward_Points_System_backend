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
    public class TransactionServiceTests
    {
        [Fact]
        public void AddTransaction_ShouldAddTransaction()
        {
            var service = new TransactionService();
            var userId = Guid.NewGuid();

            var transaction = service.AddTransaction(userId, TransactionType.Redeem, 100, "Signup bonus");

            Assert.NotNull(transaction);
            Assert.Equal(userId, transaction.UserId);
            Assert.Equal(TransactionType.Redeem, transaction.Type);
            Assert.Equal(100, transaction.Points);
            Assert.Equal("Signup bonus", transaction.Details);
        }

        [Fact]
        public void GetTransactionsByUser_ShouldReturnOnlyUserTransactions()
        {
            var service = new TransactionService();
            var user1 = Guid.NewGuid();
            var user2 = Guid.NewGuid();

            service.AddTransaction(user1, TransactionType.Redeem, 100, "Signup bonus");
            service.AddTransaction(user1, TransactionType.Earn, 50, "Purchase");
            service.AddTransaction(user2, TransactionType.Redeem, 30, "Referral bonus");

            var user1Transactions = service.GetTransactionsByUser(user1).ToList();

            Assert.Equal(2, user1Transactions.Count);
            Assert.All(user1Transactions, t => Assert.Equal(user1, t.UserId));

            var user2Transactions = service.GetTransactionsByUser(user2).ToList();
            Assert.Single(user2Transactions);
            Assert.Equal(user2, user2Transactions[0].UserId);
        }

        [Fact]
        public void GetAllTransactions_ShouldReturnAllTransactions()
        {
            var service = new TransactionService();
            var user1 = Guid.NewGuid();
            var user2 = Guid.NewGuid();

            service.AddTransaction(user1, TransactionType.Earn, 100, "Signup bonus");
            service.AddTransaction(user2, TransactionType.Earn, 30, "Referral bonus");

            var allTransactions = service.GetAllTransactions().ToList();

            Assert.Equal(2, allTransactions.Count);
        }

        [Fact]
        public void Transactions_ShouldHaveTimestamps()
        {
            var service = new TransactionService();
            var userId = Guid.NewGuid();

            var transaction = service.AddTransaction(userId, TransactionType.Earn, 100, "Signup bonus");

            Assert.True(transaction.TimeStamp <= DateTime.UtcNow);
        }
    }
}
