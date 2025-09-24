using Project.Domain.Entities.Product;
using Project.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services.Implementation
{
    public class ProductService: IProductService
    {
        private readonly IProductRepository _products;
        private readonly IProductStockRepository _productStocks;
        private readonly IProductPriceRepository _productPrices;
        public ProductService(IProductRepository products, IProductPriceRepository productPrices, IProductStockRepository productStocks)
        {
            _products = products;
            _productPrices = productPrices;
            _productStocks = productStocks;
        }

        public Product Add(string name, string description, string brand)
        {
            Product product = new Product(name, description, brand);
            _products.Add(product);
            _productPrices.Add(new ProductPrice(product.Id, 0));
            _productStocks.Add(new ProductStock(product.Id, 0));
            return product;
        }

        public void UpdateDetails(Guid productId, string name, string description, string brand)
        {
            Product? product = _products.GetById(productId);
            if (product != null)
            {
                product.UpdateDetails(name, description, brand);
                _products.Update(product);
            }
        }

        public void Deactivate(Guid productId)
        {
            Product? product = _products.GetById(productId);
            if (product != null)
            {
                product.Deactivate();
                _products.Update(product);
            }
        }

        public void Activate(Guid productId)
        {
            Product? product = _products.GetById(productId);
            if (product != null)
            {
                product.Activate();
                _products.Update(product);
            }
        }
    }
}
