using BradshawProject.Domain.Objects;
using System;
using System.Collections.Generic;

namespace BradshawProject.Domain.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        int CountTransactionsOverTheTime(DateTime dateToCompare);
        int CountSellToMerchant(string merchant);
        List<Transaction> GetLastTransactions();
        void SaveLastTransaction(Transaction lastTransaction);
        List<Transaction> SaveLastTransactions(List<Transaction> transactions);
    }
}
