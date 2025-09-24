using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities.User
{
    public class UserAccount
    {
        public Guid Id { get; }
        public Guid UserId { get; }
        public int Points { get; private set; }

        public UserAccount(Guid userId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Points = 0;
        }

        internal void AddPoints(int points)
        {
            Points += points;
        }

        internal void RedeemPoints(int points)
        {
            Points -= points;
        }
    }
}
