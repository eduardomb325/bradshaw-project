using BradshawProject.Domain.Objects;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BradshawProject.Objects
{
    public class Account
    {
        [Key]
        [Required]
        [JsonPropertyName("cardIsActive")]
        public bool CardIsActive { get; set; }

        [JsonPropertyName("blacklist")]
        public List<string> Blacklist { get; set; } = new List<string>();

        [Required]
        [JsonPropertyName("limit")]
        public double Limit { get; set; }

        [JsonPropertyName("isWhitelisted")]
        public bool IsWhitelisted { get; set; }

        public Account()
        {

        }

        public Account(bool cardIsActive, List<string> blacklist, double limit, bool isWhitelisted)
        {
            CardIsActive = cardIsActive;
            Blacklist = blacklist;
            Limit = limit;
            IsWhitelisted = isWhitelisted;
        }

        public bool IsValidLimit()
        {
            bool isValidLimit = true;

            if (Limit < 0)
            {
                isValidLimit = false;
            }

            return isValidLimit;
        }

        public RuleVerification IsBlackListNotContainsThisMerchant(string merchant)
        {
            RuleVerification response = new RuleVerification(!Blacklist.Contains(merchant), "BlackList contains the Merchant");
            
            return response;
        }

        public RuleVerification IsCardIsActive()
        {
            RuleVerification response = new RuleVerification(CardIsActive, "Card is not active");

            return response;
        }

        public void SubtractLimitValue(double transactionValue)
        {
            Limit -= transactionValue;
        }
    }
}
