using BradshawProject.Domain.Objects;
using BradshawProject.Domain.Repositories.Context;
using BradshawProject.Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BradshawProject.Domain.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApiContext _context;

        public TransactionRepository(ApiContext context)
        {
            _context = context;
        }
        public int CountSellToMerchant(string merchant)
        {
            return _context
                    .Transactions
                    .Where(x => x.Merchant.Equals(merchant))
                    .Count();
        }

        public int CountTransactionsOverTheTime(DateTime dateToCompare)
        {
            return _context.Transactions
                                    .Where(x => DateTime.Parse(x.Time) > dateToCompare)
                                    .Count();
        }

        public List<Transaction> GetLastTransactions()
        {
            return _context
                        .Transactions
                        .ToList();
        }


        public void SaveLastTransaction(Transaction transaction)
        {
            try
            {
                _context.Transactions.Add(transaction);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Transaction> SaveLastTransactions(List<Transaction> transactions)
        {
            try
            {
                _context.Transactions.AddRange(transactions);
                _context.SaveChanges();


                return transactions;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}