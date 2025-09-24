using Project.Common;
using Project.Domain.Entities.User;
using Project.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services.Implementation
{
    public class UserAccountService: IUserAccountService
    {
        public Result RedeemPoints(UserAccount account, int points)
        {
            if (points <= 0) return Result.Failure("Points must be positive");
            if (account.Points < points) return Result.Failure("Insufficient balance");

            account.RedeemPoints(points);
            return Result.Success();
        }

        public int GetCurrentPoints(UserAccount account)
        {
            return account.Points;
        }
    }
}
