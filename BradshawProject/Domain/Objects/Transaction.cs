using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BradshawProject.Domain.Objects
{
    public class Transaction
    {
        [JsonPropertyName("merchant")]
        public string Merchant { get; set; }

        [JsonPropertyName("amount")]
        public double Amount { get; set; }

        [JsonPropertyName("time")]
        public string Time { get; set; }

        private readonly IConfiguration Configuration;

        public Transaction()
        {

        }

        public RuleVerification IsTransactionOverThanLimit(double limit, double limitPercentage)
        {
            bool isTransactionOverThanLimit = false;

            double limitToCheck = limit * limitPercentage;

            if (Amount > limitToCheck)
            {
                isTransactionOverThanLimit = true;
            }

            RuleVerification response = new RuleVerification(!isTransactionOverThanLimit, "Transaction Over Than Limit");

            return response;
        }

        public RuleVerification CanThisMerchantSellsToAccount(int shopMerchantTimes, int merchantLimit)
        {
            bool isMerchantReachTheLimit = shopMerchantTimes > merchantLimit;

            RuleVerification response = new RuleVerification(!isMerchantReachTheLimit, "Shop Merchant Limit");

            return response;
        }
    }
}
