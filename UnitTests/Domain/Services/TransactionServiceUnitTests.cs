using BradshawProject.Domain.Objects;
using BradshawProject.Domain.Repositories.Interface;
using BradshawProject.Domain.Repositories.Interfaces;
using BradshawProject.Domain.Services;
using BradshawProject.Objects;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace UnitTests.Domain.Services
{
    public class TransactionServiceUnitTests
    {
        private Mock<IAccountRepository> accountRepositoryMock;
        private Mock<IConfiguration> configurationMock;
        private Mock<ITransactionRepository> transactionRepositoryMock;

        private TransactionService transactionService;

        public TransactionServiceUnitTests()
        {
            accountRepositoryMock = new Mock<IAccountRepository>();
            configurationMock = new Mock<IConfiguration>();
            transactionRepositoryMock = new Mock<ITransactionRepository>();

        }

        [Test]
        public void When_Doesnt_Have_Account_Return_Denied_Transaction()
        {
            Account account = null;

            accountRepositoryMock
                .Setup(x => x.GetAccount())
                .Returns(account);

            double amount = 50;
            string merchant = "big";
            string time = DateTime.Now.ToString();
            
            Transaction transaction = new Transaction(amount, merchant, time);

            transactionService = new TransactionService(accountRepositoryMock.Object, configurationMock.Object, transactionRepositoryMock.Object);

            LastTransaction response = transactionService.ProcessTransactionService(transaction);

            response.Approved.Should().Be(false);
            response.DeniedReasons.Contains("No account is founded.").Should().Be(true);
        }

        [Test]
        public void When_Is_First_Buy_Amount_Is_Over_Than_Limit_Return_Denied_Transaction()
        {
            Account account = new Account();

            account.CardIsActive = true;
            account.Limit = 50;

            accountRepositoryMock
                .Setup(x => x.GetAccount())
                .Returns(account);

            configurationMock.Setup(x => x["MerchantLimitSell"]).Returns("10");
            configurationMock.Setup(x => x["FirstBuyLimit"]).Returns("90");
            transactionRepositoryMock.Setup(x => x.CountSellToMerchant(It.IsAny<string>())).Returns(0);
            transactionRepositoryMock.Setup(x => x.CountTransactionsOverTheTime(It.IsAny<DateTime>())).Returns(0);
            transactionRepositoryMock.Setup(x => x.GetLastTransactions()).Returns(new List<Transaction>());

            double amount = 50;
            string merchant = "big";
            string time = DateTime.Now.ToString();

            Transaction transaction = new Transaction(amount, merchant, time);

            transactionService = new TransactionService(accountRepositoryMock.Object, configurationMock.Object, transactionRepositoryMock.Object);

            LastTransaction response = transactionService.ProcessTransactionService(transaction);

            response.Approved.Should().Be(false);
            response.DeniedReasons.Contains("Transaction Over Than Limit").Should().BeTrue();
        }

        [Test]
        public void When_Amount_Isnt_Over_Than_Limit_Return_Approve_Transaction()
        {
            Account account = new Account();

            account.CardIsActive = true;
            account.Limit = 100;

            accountRepositoryMock
                .Setup(x => x.GetAccount())
                .Returns(account);

            configurationMock.Setup(x => x["MerchantLimitSell"]).Returns("10");
            configurationMock.Setup(x => x["FirstBuyLimit"]).Returns("90");
            transactionRepositoryMock.Setup(x => x.CountSellToMerchant(It.IsAny<string>())).Returns(0);
            transactionRepositoryMock.Setup(x => x.CountTransactionsOverTheTime(It.IsAny<DateTime>())).Returns(0);
            transactionRepositoryMock.Setup(x => x.GetLastTransactions()).Returns(new List<Transaction>());

            double amount = 50;
            string merchant = "big";
            string time = DateTime.Now.ToString();

            Transaction transaction = new Transaction(amount, merchant, time);

            transactionService = new TransactionService(accountRepositoryMock.Object, configurationMock.Object, transactionRepositoryMock.Object);

            LastTransaction response = transactionService.ProcessTransactionService(transaction);

            response.Approved.Should().Be(true);
            response.NewLimit.Should().Be(50);
        }

        [Test]
        public void When_Card_Is_Block_Return_Not_Approval_Transaction()
        {
            Account account = new Account();

            account.CardIsActive = false;
            account.Limit = 100;

            accountRepositoryMock
                .Setup(x => x.GetAccount())
                .Returns(account);

            configurationMock.Setup(x => x["MerchantLimitSell"]).Returns("10");
            configurationMock.Setup(x => x["FirstBuyLimit"]).Returns("90");
            transactionRepositoryMock.Setup(x => x.CountSellToMerchant(It.IsAny<string>())).Returns(0);
            transactionRepositoryMock.Setup(x => x.CountTransactionsOverTheTime(It.IsAny<DateTime>())).Returns(0);
            transactionRepositoryMock.Setup(x => x.GetLastTransactions()).Returns(new List<Transaction>());

            double amount = 50;
            string merchant = "big";
            string time = DateTime.Now.ToString();

            Transaction transaction = new Transaction(amount, merchant, time);

            transactionService = new TransactionService(accountRepositoryMock.Object, configurationMock.Object, transactionRepositoryMock.Object);

            LastTransaction response = transactionService.ProcessTransactionService(transaction);

            
            response.Approved.Should().Be(false);
            response.DeniedReasons.Contains("Card is not active").Should().BeTrue();
        }

        [Test]
        public void When_Merchant_Limit_Sell_Over_Than_Limit_Return_Not_Approval_Transaction()
        {
            Account account = new Account();

            account.CardIsActive = false;
            account.Limit = 100;

            accountRepositoryMock
                .Setup(x => x.GetAccount())
                .Returns(account);

            configurationMock.Setup(x => x["MerchantLimitSell"]).Returns("10");
            configurationMock.Setup(x => x["FirstBuyLimit"]).Returns("90");
            transactionRepositoryMock.Setup(x => x.CountSellToMerchant(It.IsAny<string>())).Returns(10);
            transactionRepositoryMock.Setup(x => x.CountTransactionsOverTheTime(It.IsAny<DateTime>())).Returns(0);
            transactionRepositoryMock.Setup(x => x.GetLastTransactions()).Returns(new List<Transaction>());

            double amount = 50;
            string merchant = "big";
            string time = DateTime.Now.ToString();

            Transaction transaction = new Transaction(amount, merchant, time);

            transactionService = new TransactionService(accountRepositoryMock.Object, configurationMock.Object, transactionRepositoryMock.Object);

            LastTransaction response = transactionService.ProcessTransactionService(transaction);


            response.Approved.Should().Be(false);
            response.DeniedReasons.Contains("Shop Merchant Limit").Should().BeTrue();
        }

        [Test]
        public void When_Time_Limit_Sell_Over_Than_Limit_Return_Not_Approval_Transaction()
        {
            Account account = new Account();

            account.CardIsActive = false;
            account.Limit = 100;

            accountRepositoryMock
                .Setup(x => x.GetAccount())
                .Returns(account);

            configurationMock.Setup(x => x["MerchantLimitSell"]).Returns("10");
            configurationMock.Setup(x => x["FirstBuyLimit"]).Returns("90");
            transactionRepositoryMock.Setup(x => x.CountSellToMerchant(It.IsAny<string>())).Returns(0);
            transactionRepositoryMock.Setup(x => x.CountTransactionsOverTheTime(It.IsAny<DateTime>())).Returns(3);
            transactionRepositoryMock.Setup(x => x.GetLastTransactions()).Returns(new List<Transaction>());

            double amount = 50;
            string merchant = "big";
            string time = DateTime.Now.ToString();

            Transaction transaction = new Transaction(amount, merchant, time);

            transactionService = new TransactionService(accountRepositoryMock.Object, configurationMock.Object, transactionRepositoryMock.Object);

            LastTransaction response = transactionService.ProcessTransactionService(transaction);


            response.Approved.Should().Be(false);
            response.DeniedReasons.Contains("Over three transactions in 2 minutes").Should().BeTrue();
        }

        [Test]
        public void When_Merchant_Is_In_BlackList_Return_Not_Approval_Transaction()
        {
            Account account = new Account();

            account.CardIsActive = false;
            account.Limit = 100;
            account.Blacklist.Add("big");

            accountRepositoryMock
                .Setup(x => x.GetAccount())
                .Returns(account);

            configurationMock.Setup(x => x["MerchantLimitSell"]).Returns("10");
            configurationMock.Setup(x => x["FirstBuyLimit"]).Returns("90");
            transactionRepositoryMock.Setup(x => x.CountSellToMerchant(It.IsAny<string>())).Returns(0);
            transactionRepositoryMock.Setup(x => x.CountTransactionsOverTheTime(It.IsAny<DateTime>())).Returns(0);
            transactionRepositoryMock.Setup(x => x.GetLastTransactions()).Returns(new List<Transaction>());

            double amount = 50;
            string merchant = "big";
            string time = DateTime.Now.ToString();

            Transaction transaction = new Transaction(amount, merchant, time);

            transactionService = new TransactionService(accountRepositoryMock.Object, configurationMock.Object, transactionRepositoryMock.Object);

            LastTransaction response = transactionService.ProcessTransactionService(transaction);


            response.Approved.Should().Be(false);
            response.DeniedReasons.Contains("BlackList contains the Merchant").Should().BeTrue();
        }
    }
}
