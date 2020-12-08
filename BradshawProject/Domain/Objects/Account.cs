using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BradshawProject.Objects
{
    public class Account
    {
        [JsonPropertyName("cardIsActive")]
        public bool CardIsActive { get; set; }

        [JsonPropertyName("blacklist")]
        public List<string> Blacklist { get; set; }

        [JsonPropertyName("limit")]
        public double Limit { get; set; }

        [JsonPropertyName("isWhitelisted")]
        public bool IsWhitelisted { get; set; }

        public Account()
        {

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
    }
}
