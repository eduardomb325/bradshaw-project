using BradshawProject.Objects;
using NUnit.Framework;

namespace UnitTests.Domain.Objects
{
    public class AccountUnitTests
    {
        [Test]
        public void WhenLimitIsPositiveReturnsTrue()
        {
            Account account = new Account();

            account.Limit = 10;

            bool validateLimit = account.IsValidLimit();

            Assert.IsTrue(validateLimit);
        }

        [Test]
        public void WhenLimitIsNegativeReturnsFalse()
        {
            Account account = new Account();

            account.Limit = -1;

            bool validateLimit = account.IsValidLimit();

            Assert.IsFalse(validateLimit);
        }
    }
}
