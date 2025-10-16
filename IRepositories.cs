using Project.Domain.Entities;
using Project.Domain.Entities.Event;
using Project.Domain.Entities.Product;
using Project.Domain.Entities.Users;
using Project.Domain.Enums;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public interface IUserRepository
    {
        User Add(User user);
        User? GetById(Guid id);
        User? GetByEmail(string email);
        User? GetByEmployeeId(string employeeId);
        List<User> GetAll();
        void Update(User user);
    }

    public interface IUserAccountRepository
    {
        UserAccount Add(UserAccount userAccount);
        void UpdateAccount(Guid userId, int points);
        UserAccount GetAccountByUserId(Guid userId);
    }

    public interface IProductRepository
    {
        Product Add(Product p);
        Product? GetById(Guid id);
        void Update(Product p);
        void Remove(Guid id);
        List<Product> GetAll();
    }

    public interface IProductPriceRepository
    {
        ProductPrice Add(ProductPrice pp);
        ProductPrice? GetByProductId(Guid productId);
        void UpdatePoints(Guid productId, decimal newPoints);
    }

    public interface IProductStockRepository
    {
        ProductStock Add(ProductStock ps);
        ProductStock? GetByProductId(Guid productId);
        void UpdateStock(Guid productId, int newStock);
    }

    public interface IEventRepository
    {
        Event Add(Event e);
        Event? GetById(Guid id);
        List<Event> GetAll();
    }

    public interface IRedemptionRepository
    {
        Redemption Add(Redemption redemption);
        Redemption? GetById(Guid id);
        List<Redemption> GetByUserId(Guid userId);
        List<Redemption> GetPendingByProductId(Guid productId);
        void Update(Redemption r);
    }

    public interface ITransactionRepository
    {
        Transaction Add(Transaction t);
        List<Transaction> GetByUserId(Guid userId);
    }

    public class InMemoryUserRepository: IUserRepository
    {
        private static readonly ConcurrentDictionary<Guid, User> _store = new ConcurrentDictionary<Guid, User>();

        public User Add(User user)
        {
            if(_store.Values.Any(u => u.Email == user.Email ||
            u.EmployeeId == user.EmployeeId))
            {
                throw new InvalidOperationException($"User with {user.Email} already exists");
            } else
            {
                _store[user.Id] = user;
                return user;
            }
        }

        public User? GetByEmployeeId(string employeeId)
        {
            User? user = _store.Values.FirstOrDefault(u => u.EmployeeId == employeeId);
            return user;
        }

        public User? GetByEmail(string email)
        {
            User? user = _store.Values.FirstOrDefault(u => u.Email == email);
            return user;
        }

        public User? GetById(Guid id)
        {
            User? user = _store.TryGetValue(id, out var u) ? u : null;
            return user;
        }

        public List<User> GetAll()
        {
            List<User> users = _store.Values.ToList();
            return users;
        }

        public void Update(User user)
        {
            if (!_store.ContainsKey(user.Id)) throw new KeyNotFoundException("User not found");
            _store[user.Id] = user;
        }
    }

    //public class InMemoryUserAccountRepository: IUserAccountRepository
    //{
    //    private static readonly ConcurrentDictionary<Guid, User> _store = new ConcurrentDictionary<Guid, User>();
    //}

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

    public class InMemoryEventRepository : IEventRepository
    {
        private readonly ConcurrentDictionary<Guid, Event> _store = new ConcurrentDictionary<Guid, Event>();

        public Event Add(Event e)
        {
            _store[e.Id] = e;
            return e;
        }

        public Event? GetById(Guid id) => _store.TryGetValue(id, out var e) ? e : null;

        public List<Event> GetAll() => _store.Values.ToList();
    }

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
