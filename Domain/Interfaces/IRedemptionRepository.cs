using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Interfaces
{
    public interface IRedemptionRepository
    {
        Redemption Add(Redemption redemption);
        Redemption? GetById(Guid id);
        List<Redemption> GetByUserId(Guid userId);
        List<Redemption> GetPendingByProductId(Guid productId);
        void Update(Redemption r);
    }
}
