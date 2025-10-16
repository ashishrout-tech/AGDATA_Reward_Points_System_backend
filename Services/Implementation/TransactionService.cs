using Project.Domain.Entities;
using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services.Implementation
{
    public class TransactionService
    {
        private readonly List<Transaction> _transactions = new List<Transaction>();

        public Transaction AddTransaction(Guid userId, TransactionType type, int points, string details)
        {
            var transaction = new Transaction(userId, type, points, details);
            _transactions.Add(transaction);
            return transaction;
        }

        public IEnumerable<Transaction> GetTransactionsByUser(Guid userId)
        {
            return _transactions.Where(t => t.UserId == userId).ToList();
        }

        public IEnumerable<Transaction> GetAllTransactions()
        {
            return _transactions.AsReadOnly();
        }
    }
}
