using BradshawProject.Domain.Objects;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests.Domain.Objects
{
    public class TransactionUnitTests
    {
        [Test]
        public void When_Transaction_Pass_Limit_Returns_False()
        {
            Transaction transaction = new Transaction();

            transaction.Amount = 5000;

            RuleVerification ruleVerification = transaction.IsTransactionOverThanLimit(2000, 1);

            Assert.IsFalse(ruleVerification.IsValidationPass);
        }

        [Test]
        public void When_Transaction_Not_Pass_Limit_Returns_True()
        {
            Transaction transaction = new Transaction();

            transaction.Amount = 5000;

            RuleVerification ruleVerification = transaction.IsTransactionOverThanLimit(10000, 100);

            Assert.IsTrue(ruleVerification.IsValidationPass);
        }

        [Test]
        public void When_Transaction_Is_Not_Over_Than_Limit_Plus_LimitPercentage_Returns_True()
        {
            Transaction transaction = new Transaction();

            transaction.Amount = 4500;

            RuleVerification ruleVerification = transaction.IsTransactionOverThanLimit(5000, 90);

            Assert.IsTrue(ruleVerification.IsValidationPass);
        }

        [Test]
        public void When_Transaction_Is_Over_Than_Limit_Plus_LimitPercentage_Returns_False()
        {
            Transaction transaction = new Transaction();

            transaction.Amount = 4600;

            RuleVerification ruleVerification = transaction.IsTransactionOverThanLimit(5000, 90);

            Assert.IsFalse(ruleVerification.IsValidationPass);
        }

        [Test]
        public void When_Merchant_Can_Sells_Returns_True()
        {
            Transaction transaction = new Transaction();

            RuleVerification ruleVerification = transaction.CanThisMerchantSellsToAccount(5, 10);

            Assert.True(ruleVerification.IsValidationPass);
        }

        [Test]
        public void When_Merchant_Can_Sells_Returns_False()
        {
            Transaction transaction = new Transaction();

            RuleVerification ruleVerification = transaction.CanThisMerchantSellsToAccount(60, 10);

            Assert.False(ruleVerification.IsValidationPass);
        }
    }
}
