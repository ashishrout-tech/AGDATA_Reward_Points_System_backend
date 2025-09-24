using Project.Common;
using Project.Domain.Entities.User;
using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services.Interfaces
{
    public interface IAdminService
    {
        User RegisterAdmin(string name, string email, string employeeId);
        Result AssignPoints(User target, UserAccount account, int points);
        Result RedeemOwnPoints(UserAccount account, int points);
        int GetCurrentOwnPoints(UserAccount account);
        Result UpdateUserRole(User target, UserRole role);
        Result ActivateUser(User target);
        Result DeactivateUser(User target);
    }
}
