using BradshawProject.Domain.Objects;
using BradshawProject.Domain.Repositories.Interface;
using BradshawProject.Domain.Repositories.Interfaces;
using BradshawProject.Domain.Services.Interfaces;
using BradshawProject.Objects;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BradshawProject.Domain.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IAccountRepository _accountRepository;

        private readonly IConfiguration _configuration;

        private readonly ILastTransactionsRepository _lastTransactionsRepository;

        private readonly double FirstBuyLimit;

        private readonly int MerchantLimit;
        public TransactionService(IAccountRepository accountRepository,
                IConfiguration configuration,
                ILastTransactionsRepository lastTransactionsRepository
            )
        {
            _accountRepository = accountRepository;
            _lastTransactionsRepository = lastTransactionsRepository;
            _configuration = configuration;
            MerchantLimit = Convert.ToInt32(_configuration["MerchantLimitSell"]);
            FirstBuyLimit = Convert.ToInt64(_configuration["FirstBuyLimit"]) / 100;
        }
        public List<LastTransaction> GetLastTransactions()
        {
            return _lastTransactionsRepository.GetLastTransactions();
        }

        public LastTransaction ProcessTransactionService(Transaction transaction)
        {
            LastTransaction lastTransaction = (LastTransaction) transaction;

            Account account = _accountRepository.GetAccount();

           List<RuleVerification> ruleVerificationList = RuleVerifications(account, transaction, lastTransaction);

            List<string> deniedReasonsList = GetDeniedReasonsList(ruleVerificationList);

            bool isTransactionApproval = deniedReasonsList.Count().Equals(0);

            if (isTransactionApproval)
            {
                account.SubtractLimitValue(transaction.Amount);
                _accountRepository.UpdateAccount(account);

                lastTransaction.UpdateNewLimit(account.Limit);
            }

            _lastTransactionsRepository.SaveLastTransaction(lastTransaction);

            return lastTransaction;
        }


        public List<string> GetDeniedReasonsList(List<RuleVerification> ruleVerifications)
        {
            return ruleVerifications.Where(x => x.IsValidationPass.Equals(false))
                                    .Select(x => x.ErrorMessage)
                                    .ToList();
        }
        private RuleVerification CanHaveAnotherTransactionInThisMinute(Transaction transaction)
        {
            DateTime transactionDateLessTwoMinutes = DateTime.Parse(transaction.Time).AddMinutes(-2);
            int transactionsInTwoMinutes = _lastTransactionsRepository.CountTransactionsOverTheTime(transactionDateLessTwoMinutes);

            bool isThreeTransactionLastThanTwoMinutes = false;

            if (transactionsInTwoMinutes > 2)
            {
                isThreeTransactionLastThanTwoMinutes = true;
            }

            RuleVerification response = new RuleVerification(!isThreeTransactionLastThanTwoMinutes, "Over three transactions in 2 minutes");

            return response;
        }

        public List<RuleVerification> RuleVerifications(Account account, Transaction transaction, LastTransaction lastTransaction)
        {
            string merchant = transaction.Merchant;

            int sameMerchantShopTimes = _lastTransactionsRepository.CountSellToMerchant(merchant);

            List<RuleVerification> ruleVerificationList = new List<RuleVerification>();

            ruleVerificationList.Add(lastTransaction.IsTransactionOverThanLimit(account.Limit, FirstBuyLimit));

            ruleVerificationList.Add(account.IsCardIsActive());

            ruleVerificationList.Add(account.IsBlackListNotContainsThisMerchant(transaction.Merchant));

            ruleVerificationList.Add(lastTransaction.CanThisMerchantSellsToAccount(sameMerchantShopTimes, MerchantLimit));

            ruleVerificationList.Add(CanHaveAnotherTransactionInThisMinute(transaction));

            return ruleVerificationList;
        }
    }
}
