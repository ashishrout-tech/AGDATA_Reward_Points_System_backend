using Project.Common;
using Project.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services.Interfaces
{
    public interface IUserAccountService
    {
        Result RedeemPoints(UserAccount account, int points);
        int GetCurrentPoints(UserAccount account);
    }
}
