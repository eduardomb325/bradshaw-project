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

        public Transaction()
        {
               
        }
    }
}
