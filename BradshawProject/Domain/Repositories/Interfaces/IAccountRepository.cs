using BradshawProject.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BradshawProject.Domain.Repositories.Interface
{
    public interface IAccountRepository
    {
        void RegisterDataToAccount(Account account);
        Account GetAccount();
    }
}
