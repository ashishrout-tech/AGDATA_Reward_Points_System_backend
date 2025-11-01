using Project.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Interfaces
{
    public interface IUserAccountRepository
    {
        UserAccount Add(UserAccount userAccount);
        void UpdateAccount(Guid userId, int points);
        UserAccount GetAccountByUserId(Guid userId);
    }
}
