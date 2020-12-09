using BradshawProject.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BradshawProject.Domain.Repositories.Interface
{
    public interface IAccountRepository
    {
        int CountAccounts();
        Account GetAccount();
        Account RegisterDataToAccount(Account account);
        Account UpdateAccount(Account account);

        Account DeleteAccount(Account account); 
    }
}
