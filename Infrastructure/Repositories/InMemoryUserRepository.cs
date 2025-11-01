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
    public class InMemoryUserRepository : IUserRepository
    {
        private static readonly ConcurrentDictionary<Guid, User> _store = new ConcurrentDictionary<Guid, User>();

        public User Add(User user)
        {
            if (_store.Values.Any(u => u.Email == user.Email ||
            u.EmployeeId == user.EmployeeId))
            {
                throw new InvalidOperationException($"User with {user.Email} already exists");
            }
            else
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
}
