using Project.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Interfaces
{
    public interface IProductPriceRepository
    {
        ProductPrice Add(ProductPrice pp);
        ProductPrice? GetByProductId(Guid productId);
        void UpdatePoints(Guid productId, decimal newPoints);
    }
}
