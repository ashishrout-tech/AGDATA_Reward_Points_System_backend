using Microsoft.EntityFrameworkCore;
using Project.Domain.Entities;
using Project.Domain.Enums;
using Project.Domain.Interfaces;
using Project.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.Repositories
{
    public class EfRedemptionRepository : IRedemptionAsyncRepository
    {
        private readonly AppDbContext _db;

        public EfRedemptionRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Redemption> AddAsync(Redemption redemption, CancellationToken cancellationToken = default)
        {
            await _db.Redemptions.AddAsync(redemption, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            return redemption;
        }

        public async Task<Redemption?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _db.Redemptions
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        }

        public async Task<List<Redemption>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _db.Redemptions
                .AsNoTracking()
                .Where(r => r.UserId == userId)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Redemption>> GetPendingByProductIdAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            return await _db.Redemptions
                .AsNoTracking()
                .Where(r => r.ProductId == productId && r.Status == RedemptionStatus.Pending)
                .ToListAsync(cancellationToken);
        }

        public async Task UpdateAsync(Redemption redemption, CancellationToken cancellationToken = default)
        {
            var existing = await _db.Redemptions
                .FirstOrDefaultAsync(r => r.Id == redemption.Id, cancellationToken);

            if (existing == null)
                throw new KeyNotFoundException("Redemption not found.");

            _db.Entry(existing).CurrentValues.SetValues(redemption);

            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}
