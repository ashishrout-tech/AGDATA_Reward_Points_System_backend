using Project.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services.Interfaces
{
    public interface IProductPriceService
    {
        decimal GetCurrentPoints(Guid productId);
        Result UpdatePoints(Guid productId, decimal newPoints);
    }
}
