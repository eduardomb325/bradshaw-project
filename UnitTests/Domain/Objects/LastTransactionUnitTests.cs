using BradshawProject.Domain.Objects;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Domain.Objects
{
    public class LastTransactionUnitTests
    {
        [Test]
        public void When_New_Limit_Updates_Returns_New_Limit()
        {
            LastTransaction lastTransaction = new LastTransaction();

            lastTransaction.NewLimit = 10;

            lastTransaction.UpdateNewLimit(100);

            Assert.IsTrue(lastTransaction.NewLimit.Equals(100));
        }
    }
}
