using BradshawProject.Domain.Repositories.Context;
using BradshawProject.Domain.Repositories.Interface;
using BradshawProject.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BradshawProject.Domain.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApiContext _context;
        public AccountRepository(ApiContext context)
        {
            _context = context;
        }

        public int CountAccounts()
        {
            return _context.Accounts.Count();
        }

        public Account GetAccount()
        {
            try
            {
                Account account = _context.Accounts.FirstOrDefault();

                return account;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Account RegisterDataToAccount(Account account)
        {
            try
            {
                _context.Accounts.Add(account);
                _context.SaveChanges();

                return account;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Account UpdateAccount(Account account)
        {
            try
            {
                _context.Accounts.Update(account);
                _context.SaveChanges();

                return account;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
