using Project.Domain.Entities.Product;
using Project.Domain.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.Repositories
{
    public class InMemoryProductRepository : IProductRepository
    {
        private readonly ConcurrentDictionary<Guid, Product> _store = new ConcurrentDictionary<Guid, Product>();

        public Product Add(Product p)
        {
            _store[p.Id] = p;
            return p;
        }

        public List<Product> GetAll() => _store.Values.ToList();

        public Product? GetById(Guid id) => _store.TryGetValue(id, out var p) ? p : null;

        public void Remove(Guid id)
        {
            _store.TryRemove(id, out _);
        }

        public void Update(Product p)
        {
            if (!_store.ContainsKey(p.Id)) throw new KeyNotFoundException("Product not found");
            _store[p.Id] = p;
        }
    }
}
