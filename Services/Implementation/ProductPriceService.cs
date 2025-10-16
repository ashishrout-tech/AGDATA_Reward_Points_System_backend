using Project.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services.Implementation
{
    public class ProductPriceService
    {
        private readonly IProductPriceRepository _productPrices;
        public ProductPriceService(IProductPriceRepository productPrices)
        {
            _productPrices = productPrices;
        }
        public decimal GetCurrentPrice(Guid productId)
        {
            ProductPrice? productPrice = _productPrices.GetByProductId(productId);
            return productPrice?.CurrentPoints ?? 0;
        }
        public void UpdatePrice(Guid productId, decimal newPrice)
        {
            ProductPrice? productPrice = _productPrices.GetByProductId(productId);
            if (productPrice != null)
            {
                productPrice.UpdatePoints(newPrice);
                _productPrices.UpdatePoints(productId, newPrice);
            }
        }
    }
}
