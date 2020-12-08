using BradshawProject.Objects;
using BradshawProject.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BradshawProject.Services
{
    public class AccountService : IAccountService
    {
        public AccountService()
        {
             
        }

        public Account GetAccountData()
        {
            throw new NotImplementedException();
        }

        public void RegisterDataToAccount(Account account)
        {
            if (!account.IsValidLimit())
            {
                
            }


            throw new NotImplementedException();
        }
    }
}
