using System;
using System.Collections.Generic;
using Project.Domain.Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Interfaces
{
    public interface ITransactionRepository
    {
        Transaction Add(Transaction t);
        List<Transaction> GetByUserId(Guid userId);
    }

    public interface ITransactionAsyncRepository
    {
        Task<Transaction> AddAsync(Transaction transaction, CancellationToken cancellationToken = default);
        Task<List<Transaction>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
