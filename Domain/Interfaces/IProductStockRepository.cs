using Project.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Interfaces
{
    public interface IProductStockRepository
    {
        ProductStock Add(ProductStock ps);
        ProductStock? GetByProductId(Guid productId);
        void UpdateStock(Guid productId, int newStock);
    }
}
