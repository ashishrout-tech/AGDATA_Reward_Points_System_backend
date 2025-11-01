using Project.Domain.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Interfaces
{
    public interface IProductRepository
    {
        Product Add(Product p);
        Product? GetById(Guid id);
        void Update(Product p);
        void Remove(Guid id);
        List<Product> GetAll();
    }
}
