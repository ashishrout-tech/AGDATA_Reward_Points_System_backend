using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Domain.Entities.Users;

namespace Project.Services.Interfaces
{
    public interface IUserService
    {
        User RegisterUser(string name, string email, string employeeId, UserRole role = UserRole.EMPLOYEE);
    }
}
