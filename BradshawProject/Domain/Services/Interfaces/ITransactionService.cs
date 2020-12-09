using BradshawProject.Domain.Objects;
using BradshawProject.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BradshawProject.Domain.Services.Interfaces
{
    public interface ITransactionService
    {
        List<LastTransaction> GetLastTransactions();
        LastTransaction ProcessTransactionService(Transaction transaction);
        List<RuleVerification> RuleVerifications(Account account, Transaction transaction, LastTransaction lastTransaction);
    }
}
