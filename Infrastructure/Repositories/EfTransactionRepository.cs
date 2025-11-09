using Microsoft.EntityFrameworkCore;
using Project.Domain.Interfaces;
using Project.Infrastructure.Data;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.Repositories
{
    public class EfTransactionRepository : ITransactionAsyncRepository
    {
        private readonly AppDbContext _db;

        public EfTransactionRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Transaction> AddAsync(Transaction transaction, CancellationToken cancellationToken = default)
        {
            await _db.Transactions.AddAsync(transaction, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            return transaction;
        }

        public async Task<List<Transaction>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _db.Transactions
                .AsNoTracking()
                .Where(t => t.UserId == userId)
                .ToListAsync(cancellationToken);
        }
    }
}
