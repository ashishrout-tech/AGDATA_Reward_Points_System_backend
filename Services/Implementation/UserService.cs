using Project.Domain.Entities.Users;
using Project.Domain.Enums;
using Project.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services.Implementation
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _users;
        private readonly IUserAccountRepository _usersAccount;

        public UserService(IUserRepository users, IUserAccountRepository usersAccount)
        {
            _users = users;
            _usersAccount = usersAccount;
        }

        public User RegisterUser(string name, string email, string employeeId, UserRole role = UserRole.EMPLOYEE)
        {
            User user = new User(name, email, employeeId, role);
            _users.Add(user);
            CreateUserAccount(user);
            return user;
        }

        public int GetCurrentPoints(UserAccount account)
        {
            return account.Points;
        }

        private void CreateUserAccount(User user)
        {
            UserAccount userAccount = new UserAccount(user.Id);
            _usersAccount.Add(userAccount);
        }
    }
}
