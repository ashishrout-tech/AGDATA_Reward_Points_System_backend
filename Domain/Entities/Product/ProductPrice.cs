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
        public decimal CurrentPoints { get; private set; }
        public DateTime CreatedAt { get; }
        public DateTime UpdatedAt { get; private set; }

        public ProductPrice(Guid productId, decimal initialPoints)
        {
            ProductId = productId;
            CurrentPoints = initialPoints;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdatePoints(decimal newPoints)
        {
            CurrentPoints = newPoints;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
