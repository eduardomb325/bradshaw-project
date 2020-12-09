using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BradshawProject.Domain.Objects
{
    public class Transaction
    {
        [Required]
        [JsonPropertyName("amount")]
        public double Amount { get; set; }

        [Required]
        [JsonPropertyName("merchant")]
        public string Merchant { get; set; }

        [Key]
        [JsonPropertyName("time")]
        [Required]
        public string Time { get; set; }

        private readonly IConfiguration Configuration;

        public Transaction()
        {

        }

        public Transaction(double amount, string merchant, string time)
        {
            Amount = amount;
            Merchant = merchant;
            Time = time;
        }

        public RuleVerification IsTransactionOverThanLimit(double limit, double limitPercentage)
        {
            bool isTransactionOverThanLimit = false;

            double limitToCheck = limit * (limitPercentage / 100);

            if (Amount > limitToCheck)
            {
                isTransactionOverThanLimit = true;
            }

            RuleVerification response = new RuleVerification(!isTransactionOverThanLimit, "Transaction Over Than Limit");

            return response;
        }

        public RuleVerification CanThisMerchantSellsToAccount(int shopMerchantTimes, int merchantLimit)
        {
            bool isMerchantReachTheLimit = shopMerchantTimes >= merchantLimit;

            RuleVerification response = new RuleVerification(!isMerchantReachTheLimit, "Shop Merchant Limit");

            return response;
        }
    }
}
