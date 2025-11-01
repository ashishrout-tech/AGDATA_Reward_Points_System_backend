using Project.Domain.Entities.Users;
using Project.Domain.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.Repositories
{
    public class InMemoryUserAccountRepository : IUserAccountRepository
    {
        private static readonly ConcurrentDictionary<Guid, UserAccount> _store = new();

        public UserAccount Add(UserAccount userAccount)
        {
            if (_store.ContainsKey(userAccount.UserId))
                throw new InvalidOperationException($"Account for user {userAccount.UserId} already exists.");

            _store[userAccount.UserId] = userAccount;
            return userAccount;
        }

        public void UpdateAccount(Guid userId, int points)
        {
            if (!_store.TryGetValue(userId, out var account))
                throw new KeyNotFoundException("User account not found.");
            account.AddPoints(points);

            _store[userId] = account;
        }

        public UserAccount GetAccountByUserId(Guid userId)
        {
            if (!_store.TryGetValue(userId, out var account))
                throw new KeyNotFoundException("User account not found.");

            return account;
        }
    }
}
