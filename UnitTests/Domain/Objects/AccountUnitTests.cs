using BradshawProject.Objects;
using NUnit.Framework;
using System.Collections.Generic;

namespace UnitTests.Domain.Objects
{
    public class AccountUnitTests
    {

        [Test]
        public void When_BlackList_Has_This_Merchant_Returns_True()
        {
            List<string> blacklist = new List<string>();

            blacklist.Add("barateiro");
            blacklist.Add("big");
            blacklist.Add("extra");

            Account account = new Account();

            account.Blacklist = blacklist;

            bool isBlackListContainsThisMerchant = account.IsBlackListContainsThisMerchant("extra");

            Assert.IsTrue(isBlackListContainsThisMerchant);
        }

        [Test]
        public void When_BlackList_Hasnt_Contains_This_Merchant_Returns_False()
        {
            List<string> blacklist = new List<string>();

            blacklist.Add("barateiro");
            blacklist.Add("big");
            blacklist.Add("extra");

            Account account = new Account();

            account.Blacklist = blacklist;

            bool isBlackListContainsThisMerchant = account.IsBlackListContainsThisMerchant("dia");

            Assert.IsFalse(isBlackListContainsThisMerchant);
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
    }
}
