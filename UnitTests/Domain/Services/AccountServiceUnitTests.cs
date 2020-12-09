using BradshawProject.Domain.Repositories;
using BradshawProject.Domain.Repositories.Interface;
using BradshawProject.Objects;
using BradshawProject.Services;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Domain.Services
{
    public class AccountServiceUnitTests
    {
        public AccountService accountService;
        public Mock<IAccountRepository> repositoryMock;

        public AccountServiceUnitTests()
        {
            repositoryMock = new Mock<IAccountRepository>();
            accountService = new AccountService(repositoryMock.Object);
        }

        [Test]
        public void When_Have_Account_Register_Her()
        {
            Account account = new Account();

            account.CardIsActive = true;

            account.Limit = 100;

            repositoryMock.Setup(x => x.RegisterDataToAccount(It.IsAny<Account>()))
                .Returns(account);

            var response = accountService.RegisterDataToAccount(account);

            response.Should().Be(account);
        }

        [Test]
        public void When_Have_Account_Get_Account_Data()
        {
            Account account = new Account();

            account.CardIsActive = true;

            account.Limit = 100;

            repositoryMock.Setup(x => x.GetAccount())
                .Returns(account);

            var response = accountService.GetAccountData();

            response.Should().Be(account);
        }
    }
}
