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

        private readonly ITransactionRepository _transactionsRepository;

        private readonly double FirstBuyLimit;

        private readonly int MerchantLimit;
        public TransactionService(IAccountRepository accountRepository,
                IConfiguration configuration,
                ITransactionRepository transactionsRepository
            )
        {
            _accountRepository = accountRepository;
            _transactionsRepository = transactionsRepository;
            _configuration = configuration;
            MerchantLimit = Convert.ToInt32(_configuration["MerchantLimitSell"]);
            FirstBuyLimit = Convert.ToInt64(_configuration["FirstBuyLimit"]);
        }
        public List<Transaction> GetLastTransactions()
        {
            return _transactionsRepository.GetLastTransactions();
        }

        public List<Transaction> SetLastTransactions(List<Transaction> transactions)
        {
            return _transactionsRepository.SaveLastTransactions(transactions);
        }

        public LastTransaction ProcessTransactionService(Transaction transaction)
        {
            LastTransaction lastTransaction = new LastTransaction();

            Account account = _accountRepository.GetAccount();

            if (account != null)
            {
                List<RuleVerification> ruleVerificationList = RuleVerifications(account, transaction);

                List<string> deniedReasonsList = GetDeniedReasonsList(ruleVerificationList);

                bool isTransactionApproval = deniedReasonsList.Count().Equals(0);

                if (isTransactionApproval)
                {
                    account.SubtractLimitValue(transaction.Amount);
                    _accountRepository.UpdateAccount(account);

                    lastTransaction.UpdateNewLimit(account.Limit);
                    _transactionsRepository.SaveLastTransaction(transaction);
                }

                lastTransaction.Approved = isTransactionApproval;
                lastTransaction.DeniedReasons.AddRange(deniedReasonsList);
            }
            else
            {
                lastTransaction = GenerateEmptyAccountResponse();
            }

            return lastTransaction;
        }


        public List<string> GetDeniedReasonsList(List<RuleVerification> ruleVerifications)
        {
            return ruleVerifications.Where(x => x.IsValidationPass == false)
                                    .Select(x => x.ErrorMessage)
                                    .ToList();
        }

        public LastTransaction GenerateEmptyAccountResponse()
        {
            LastTransaction lastTransaction = new LastTransaction();

            lastTransaction.Approved = false;
            lastTransaction.DeniedReasons.Add("No account is founded.");

            return lastTransaction;
        }

        private RuleVerification CanHaveAnotherTransactionInThisMinute(Transaction transaction)
        {
            DateTime transactionDateLessTwoMinutes = DateTime.Parse(transaction.Time).AddMinutes(-2);
            int transactionsInTwoMinutes = _transactionsRepository.CountTransactionsOverTheTime(transactionDateLessTwoMinutes);

            bool isThreeTransactionLastThanTwoMinutes = false;

            if (transactionsInTwoMinutes > 2)
            {
                isThreeTransactionLastThanTwoMinutes = true;
            }

            RuleVerification response = new RuleVerification(!isThreeTransactionLastThanTwoMinutes, "Over three transactions in 2 minutes");

            return response;
        }

        public List<RuleVerification> RuleVerifications(Account account, Transaction transaction)
        {
            string merchant = transaction.Merchant;

            int sameMerchantShopTimes = _transactionsRepository.CountSellToMerchant(merchant);

            List<RuleVerification> ruleVerificationList = new List<RuleVerification>();

            ruleVerificationList.Add(transaction.IsTransactionOverThanLimit(account.Limit, FirstBuyLimit));

            ruleVerificationList.Add(account.IsCardIsActive());

            ruleVerificationList.Add(account.IsBlackListNotContainsThisMerchant(transaction.Merchant));

            ruleVerificationList.Add(transaction.CanThisMerchantSellsToAccount(sameMerchantShopTimes, MerchantLimit));

            ruleVerificationList.Add(CanHaveAnotherTransactionInThisMinute(transaction));

            return ruleVerificationList;
        }
    }
}
