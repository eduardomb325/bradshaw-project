using BradshawProject.Domain.Objects;
using System;
using System.Collections.Generic;

namespace BradshawProject.Domain.Repositories.Interfaces
{
    public interface ILastTransactionsRepository
    {
        int CountTransactionsOverTheTime(DateTime dateToCompare);
        int CountSellToMerchant(string merchant);
        List<LastTransaction> GetLastTransactions();
        void SaveLastTransaction(LastTransaction lastTransaction);
    }
}
