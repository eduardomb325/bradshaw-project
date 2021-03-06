﻿using BradshawProject.Domain.Repositories.Interface;
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
        private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public Account GetAccountData()
        {
            return _accountRepository.GetAccount();
        }

        public Account RegisterDataToAccount(Account account)
        {
            try
            {
                var isAnotherAccount = _accountRepository.CountAccounts();

                if (isAnotherAccount > 0)
                {
                    Account accountToDelete = _accountRepository.GetAccount();
                    _accountRepository.DeleteAccount(accountToDelete);
                }
                
                _accountRepository.RegisterDataToAccount(account);

                return account;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
