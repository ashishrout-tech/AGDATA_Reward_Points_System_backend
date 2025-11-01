using Project.Domain.Interfaces;
using Project.Domain.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.Repositories
{
    public class InMemoryTransactionRepository : ITransactionRepository
    {
        private readonly ConcurrentDictionary<Guid, Transaction> _store = new ConcurrentDictionary<Guid, Transaction>();

        public Transaction Add(Transaction t)
        {
            _store[t.Id] = t;
            return t;
        }

        public List<Transaction> GetByUserId(Guid userId) => _store.Values.Where(x => x.UserId == userId).ToList();
    }
}
