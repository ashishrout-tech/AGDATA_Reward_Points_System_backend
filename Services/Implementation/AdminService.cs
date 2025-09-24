using Project.Common;
using Project.Domain.Entities.User;
using Project.Domain.Enums;
using Project.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services.Implementation
{
    public class AdminService: IAdminService
    {
        private readonly IUserService _userService;
        private readonly IUserAccountService _userAccountService;

        public AdminService(IUserService userService, IUserAccountService userAccountService)
        {
            _userService = userService;
            _userAccountService = userAccountService;
        }

        public User RegisterAdmin(string name, string email, string employeeId)
        {
            User admin = _userService.RegisterUser(name, email, employeeId, UserRole.ADMIN);
            return admin;
        }

        public Result AssignPoints(User target, UserAccount account, int points)
        {
            if (target == null) return Result.Failure("User cannot be null");
            if (!target.IsActive) return Result.Failure("Inactive user cannot earn points");
            if (points <= 0) return Result.Failure("Points must be positive");

            account.AddPoints(points);
            return Result.Success();
        }

        public Result RedeemOwnPoints(UserAccount account, int points)
        {
            return _userAccountService.RedeemPoints(account, points);
        }

        public int GetCurrentOwnPoints(UserAccount account)
        {
            return _userAccountService.GetCurrentPoints(account);
        }

        public Result UpdateUserRole(User target, UserRole role)
        {
            if (target == null) return Result.Failure("User cannot be null");
            target.UpdateUserRole(role);
            return Result.Success();
        }

        public Result ActivateUser(User target)
        {
            if (target.IsActive) return Result.Failure("User is already active");
            target.ActivateUser();
            return Result.Success();
        }

        public Result DeactivateUser(User target)
        {
            if (!target.IsActive) return Result.Failure("User is already inactive");
            target.DeactivateUser();
            return Result.Success();
        }
    }
}
