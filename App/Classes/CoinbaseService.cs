using App.Models.Coinbase;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Classes
{
    public class CoinbaseService : ICoinbaseService
    {
        public CoinbaseExchangeRate ExchangeRate(string currency)
        {
            var client = new RestClient("https://api.coinbase.com/v2");
            var request = new RestRequest($"exchange-rates?currency={currency.ToUpper()}", Method.GET);

            var response = client.Execute(request);
            var stringResult = response.Content;
            var result = JsonConvert.DeserializeObject<CoinbaseRequestResult<CoinbaseExchangeRate>>(stringResult);

            return result?.Data;
        }

        private class CoinbaseRequestResult<T>
        {
            public T Data { get; set; }
        }
    }

    public interface ICoinbaseService
    {
        CoinbaseExchangeRate ExchangeRate(string currency);
    }
}
