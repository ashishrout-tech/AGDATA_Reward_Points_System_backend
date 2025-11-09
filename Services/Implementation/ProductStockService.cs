using Project.Domain.Entities.Product;
using Project.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services.Implementation
{
    public class ProductStockService
    {
        private readonly IProductStockRepository _productStocks;
        public ProductStockService(IProductStockRepository productStocks)
        {
            _productStocks = productStocks;
        }
        public int GetCurrentStock(Guid productId)
        {
            ProductStock? productStock = _productStocks.GetByProductId(productId);
            return productStock?.AvailableStock ?? 0;
        }
        public void UpdateStock(Guid productId, int newStock)
        {
            ProductStock? productStock = _productStocks.GetByProductId(productId);
            if (productStock != null)
            {
                productStock.UpdateStock(newStock);
                _productStocks.UpdateStock(productId, newStock);
            }
        }
    }
}
