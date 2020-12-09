using BradshawProject.Domain.Objects;
using BradshawProject.Objects;
using NUnit.Framework;
using System.Collections.Generic;

namespace UnitTests.Domain.Objects
{
    public class AccountUnitTests
    {

        [Test]
        public void When_BlackList_Has_This_Merchant_Returns_False()
        {
            List<string> blacklist = new List<string>();

            blacklist.Add("barateiro");
            blacklist.Add("big");
            blacklist.Add("extra");

            Account account = new Account();

            account.Blacklist = blacklist;

            RuleVerification isBlackListContainsThisMerchant = account.IsBlackListNotContainsThisMerchant("extra");

            Assert.IsFalse(isBlackListContainsThisMerchant.IsValidationPass);
        }

        [Test]
        public void When_BlackList_Hasnt_Contains_This_Merchant_Returns_True()
        {
            List<string> blacklist = new List<string>();

            blacklist.Add("barateiro");
            blacklist.Add("big");
            blacklist.Add("extra");

            Account account = new Account();

            account.Blacklist = blacklist;

            RuleVerification isBlackListContainsThisMerchant = account.IsBlackListNotContainsThisMerchant("dia");

            Assert.True(isBlackListContainsThisMerchant.IsValidationPass);
        }

        [Test]
        public void When_Is_Card_Is_Actived_Returns_True()
        {
            Account account = new Account();

            account.CardIsActive = true;

            RuleVerification validateCardActive = account.IsCardIsActive();

            Assert.IsTrue(validateCardActive.IsValidationPass);
        }

        [Test]
        public void When_Is_Card_Isnt_Actived_Returns_False()
        {
            Account account = new Account();

            account.CardIsActive = false;

            RuleVerification validateCardActive = account.IsCardIsActive();

            Assert.IsFalse(validateCardActive.IsValidationPass);
        }

        [Test]
        public void When_Limit_Is_Positive_Returns_True()
        {
            Account account = new Account();

            account.Limit = 10;

            bool validateLimit = account.IsValidLimit();

            Assert.IsTrue(validateLimit);
        }

        [Test]
        public void When_Limit_Is_Negative_Returns_False()
        {
            Account account = new Account();

            account.Limit = -1;

            bool validateLimit = account.IsValidLimit();

            Assert.IsFalse(validateLimit);
        }

        [Test]
        public void When_Subtract_Limit_Return_Updated_Limit() 
        {
            Account account = new Account();

            account.Limit = 10;

            account.SubtractLimitValue(5);

            Assert.IsTrue(account.Limit.Equals(5));
        }

    }
}
