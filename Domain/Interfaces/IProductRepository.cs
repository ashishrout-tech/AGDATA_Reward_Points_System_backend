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

    public interface IProductAsyncRepository
    {
        Task<Product> AddAsync(Product product, CancellationToken cancellationToken = default);
        Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task UpdateAsync(Product product, CancellationToken cancellationToken = default);
        Task RemoveAsync(Guid id, CancellationToken cancellationToken = default);
        Task<List<Product>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
