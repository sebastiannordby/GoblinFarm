using App.Models.HiveOn;
using Newtonsoft.Json;
using RestSharp;

namespace App.Services
{
    public class HiveOnService : IHiveOnService
    {
        public HiveOnEarnings GetEarnings(string walletAddress, string coin)
        {
            var walletAddressSub = walletAddress?.Substring(2)?.ToLower();
            var client = new RestClient("https://hiveon.net/api/v1/stats/miner");
            var request = new RestRequest($"{walletAddressSub}/{coin}/billing-acc", Method.GET);

            request.AddHeader("Content-Type", "application/json");
            request.RequestFormat = DataFormat.Json;

            var response = client.Execute(request);
            var stringResult = response.Content;

            return JsonConvert.DeserializeObject<HiveOnEarnings>(stringResult);
        }
    }

    public interface IHiveOnService
    {
        HiveOnEarnings GetEarnings(string walletAddress, string coin);
    }
}
