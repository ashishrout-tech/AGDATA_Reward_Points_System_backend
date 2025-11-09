using Project.Domain.Entities;
using Project.Domain.Enums;
using Project.Domain.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.Repositories
{
    public class InMemoryRedemptionRepository : IRedemptionRepository
    {
        private readonly ConcurrentDictionary<Guid, Redemption> _store = new ConcurrentDictionary<Guid, Redemption>();

        public Redemption Add(Redemption r)
        {
            _store[r.Id] = r;
            return r;
        }

        public Redemption? GetById(Guid id) => _store.TryGetValue(id, out var r) ? r : null;

        public List<Redemption> GetByUserId(Guid userId) => _store.Values.Where(x => x.UserId == userId).ToList();

        public List<Redemption> GetPendingByProductId(Guid productId) =>
            _store.Values.Where(x => x.ProductId == productId && x.Status == RedemptionStatus.Pending).ToList();

        public void Update(Redemption r)
        {
            if (!_store.ContainsKey(r.Id)) throw new KeyNotFoundException("Redemption not found");
            _store[r.Id] = r;
        }
    }
}
