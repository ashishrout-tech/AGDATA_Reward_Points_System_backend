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
}
