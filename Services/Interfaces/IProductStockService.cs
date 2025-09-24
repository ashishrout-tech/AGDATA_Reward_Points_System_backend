using Project.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services.Interfaces
{
    public interface IProductStockService
    {
        int GetCurrentStock(Guid productId);
        Result UpdateStock(Guid productId, int newStock);
    }
}
