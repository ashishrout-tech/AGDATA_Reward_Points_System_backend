using Microsoft.EntityFrameworkCore;
using Project.Domain.Entities.Product;
using Project.Domain.Interfaces;
using Project.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.Repositories
{
    public class EfProductRepository : IProductAsyncRepository
    {
        private readonly AppDbContext _db;

        public EfProductRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Product> AddAsync(Product product, CancellationToken cancellationToken = default)
        {
            await _db.Products.AddAsync(product, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            return product;
        }

        public async Task<List<Product>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _db.Products
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _db.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public async Task RemoveAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var product = await _db.Products.FindAsync(new object[] { id }, cancellationToken);
            if (product == null)
                throw new KeyNotFoundException("Product not found.");

            _db.Products.Remove(product);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Product product, CancellationToken cancellationToken = default)
        {
            var existing = await _db.Products
                .FirstOrDefaultAsync(p => p.Id == product.Id, cancellationToken);

            if (existing == null)
                throw new KeyNotFoundException("Product not found.");

            _db.Entry(existing).CurrentValues.SetValues(product);
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}
