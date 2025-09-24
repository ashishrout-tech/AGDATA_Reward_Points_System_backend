using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Entities.Product
{
    public class ProductPrice
    {
        public Guid ProductId { get; }
        public decimal CurrentPrice { get; private set; }
        public string Currency { get; }

        public ProductPrice(Guid productId, decimal initialPrice, string currency = "INR")
        {
            ProductId = productId;
            CurrentPrice = initialPrice;
            Currency = currency;
        }

        public void UpdatePrice(decimal newPrice)
        {
            CurrentPrice = newPrice;
        }
    }
}
