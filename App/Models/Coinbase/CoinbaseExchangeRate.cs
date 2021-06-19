using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models.Coinbase
{
    public class CoinbaseExchangeRate
    {
        public string Currency { get; set; }
        public Dictionary<string, decimal> Rates { get; set; }
    }
}
