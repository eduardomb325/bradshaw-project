using BradshawProject.Domain.Objects;
using BradshawProject.Domain.Repositories.Context;
using BradshawProject.Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BradshawProject.Domain.Repositories
{
    public class LastTransactionsRepository : ILastTransactionsRepository
    {
        private readonly ApiContext _context;

        public LastTransactionsRepository(ApiContext context)
        {
            _context = context;
        }
        public int CountSellToMerchant(string merchant)
        {
            return _context
                    .LastTransactions
                    .Where(x => x.Merchant.Equals(merchant))
                    .Count();
        }

        public int CountTransactionsOverTheTime(DateTime dateToCompare)
        {
            return _context.LastTransactions
                                    .Where(x => DateTime.Parse(x.Time) > dateToCompare)
                                    .Count();
        }

        public List<LastTransaction> GetLastTransactions()
        {
            return _context
                        .LastTransactions
                        .ToList();
        }


        public void SaveLastTransaction(LastTransaction lastTransaction)
        {
            try
            {
                _context.LastTransactions.Add(lastTransaction);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}