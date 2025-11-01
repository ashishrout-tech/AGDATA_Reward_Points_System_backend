using Project.Domain.Entities.Users;
using Project.Domain.Enums;
using Project.Services.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Tests
{
    public class FakeUserRepository : IUserRepository
    {
        public List<User> Users = new List<User>();
        public void Add(User user) => Users.Add(user);

        public List<User> GetAll()
        {
            return Users;
        }

        public User? GetByEmail(string email)
        {
            return Users.FirstOrDefault(u => u.Email == email);
        }

        public User? GetByEmployeeId(string employeeId)
        {
            return Users.FirstOrDefault(u => u.EmployeeId == employeeId);
        }

        public User? GetById(Guid id)
        {
            return Users.FirstOrDefault(u => u.Id == id);
        }

        public void Update(User user)
        {
            int index = Users.FindIndex(u => u.Id == user.Id);
            if (index != -1)
            {
                Users[index] = user;
            }
        }

        User IUserRepository.Add(User user)
        {
            Users.Add(user);
            return user;
        }
    }

    public class FakeUserAccountRepository : IUserAccountRepository
    {
        public List<UserAccount> Accounts = new List<UserAccount>();
        public void Add(UserAccount account) => Accounts.Add(account);

        public UserAccount GetAccountByUserId(Guid userId)
        {
            return Accounts.FirstOrDefault(a => a.UserId == userId);
        }

        public void UpdateAccount(Guid userId, int points)
        {
            UserAccount account = GetAccountByUserId(userId);
            if (account != null)
            {
                account.AddPoints(points);
            }
        }

        UserAccount IUserAccountRepository.Add(UserAccount userAccount)
        {
            Accounts.Add(userAccount);
            return userAccount;
        }
    }

    public class UserServiceTest
    {
        public static void Main()
        {
            var userRepo = new FakeUserRepository();
            var accountRepo = new FakeUserAccountRepository();
            var userService = new UserService(userRepo, accountRepo);

            TestRegisterUser(userService, userRepo, accountRepo);
            TestDefaultRole(userService);
            TestGetCurrentPoints(userService);
        }

        private static void TestRegisterUser(UserService service, FakeUserRepository userRepo, FakeUserAccountRepository accountRepo)
        {
            var user = service.RegisterUser("John Doe", "john@example.com", "EMP001", UserRole.ADMIN);

            Console.WriteLine(user.Name == "John Doe" ? "PASS" : "FAIL");
            Console.WriteLine(userRepo.Users.Count == 1 ? "PASS" : "FAIL");
            Console.WriteLine(accountRepo.Accounts.Count == 1 ? "PASS" : "FAIL");
        }

        private static void TestDefaultRole(UserService service)
        {
            var user = service.RegisterUser("Jane Doe", "jane@example.com", "EMP002");
            Console.WriteLine(user.Role == UserRole.EMPLOYEE ? "PASS" : "FAIL");
        }

        private static void TestGetCurrentPoints(UserService service)
        {
            var user = service.RegisterUser("Sam Smith", "sam@example.com", "EMP003");
            var account = new UserAccount(user.Id);
            account.AddPoints(100);
            var points = service.GetCurrentPoints(account);

            Console.WriteLine(points == 100 ? "PASS" : "FAIL");
        }
    }
}
