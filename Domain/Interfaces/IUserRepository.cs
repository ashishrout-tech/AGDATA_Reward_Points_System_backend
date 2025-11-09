using Project.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Interfaces
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

    public interface IUserAsyncRepository
    {
        Task<User> AddAsync(User user, CancellationToken cancellationToken = default);
        Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<User?> GetByEmployeeIdAsync(string employeeId, CancellationToken cancellationToken = default);
        Task<List<User>> GetAllAsync(CancellationToken cancellationToken = default);
        Task UpdateAsync(User user, CancellationToken cancellationToken = default);
    }
}
