using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities.Product
{
    public class ProductStock
    {
        public Guid ProductId { get; }
        public int AvailableStock { get; private set; }

        public ProductStock(Guid productId, int initialStock)
        {
            ProductId = productId;
            AvailableStock = initialStock;
        }

        public void UpdateStock(int newStock)
        {
            AvailableStock = newStock;
        }
    }
}
