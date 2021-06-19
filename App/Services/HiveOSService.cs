using App.Classes;
using GoblinFarm.Models;
using GoblinFarm.Models.HiveOS;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;

namespace GoblinFarm.Services
{
    public class HiveOSService : IHiveOSService
    {
        public string GetAccessToken()
        {
            return $"Bearer {SystemVariables.HiveOSSettings.ApiKey}";
        }

        public List<HiveOSFarm> GetFarms()
        {
            var client = new RestClient(SystemVariables.HiveOSSettings.ApiPrefix);
            var request = new RestRequest("farms", Method.GET);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", GetAccessToken());
            request.RequestFormat = DataFormat.Json;

            var response = client.Execute(request);
            var stringResult = response.Content;
            var result = JsonConvert.DeserializeObject<HiveResult<List<HiveOSFarm>>>(stringResult);

            return result.Data;
        }

        public List<HiveOSWorker> GetWorkers(int farmId)
        {
            var client = new RestClient(SystemVariables.HiveOSSettings.ApiPrefix);
            var request = new RestRequest($"farms/{farmId}/workers", Method.GET);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", GetAccessToken());
            request.RequestFormat = DataFormat.Json;

            var response = client.Execute(request);
            var stringResult = response.Content;
            var result = JsonConvert.DeserializeObject<HiveResult<List<HiveOSWorker>>>(stringResult);

            return result.Data;
        }

        public List<HiveOSWallet> GetWallets(int farmId)
        {
            var client = new RestClient(SystemVariables.HiveOSSettings.ApiPrefix);
            var request = new RestRequest($"farms/{farmId}/wallets", Method.GET);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", GetAccessToken());
            request.RequestFormat = DataFormat.Json;

            var response = client.Execute(request);
            var stringResult = response.Content;
            var result = JsonConvert.DeserializeObject<HiveResult<List<HiveOSWallet>>>(stringResult);

            return result.Data;
        }

        public HiveOSWallet GetWallet(int walletId)
        {
            var client = new RestClient(SystemVariables.HiveOSSettings.ApiPrefix);
            var request = new RestRequest($"wallets/{walletId}", Method.GET);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", GetAccessToken());
            request.RequestFormat = DataFormat.Json;

            var response = client.Execute(request);
            var stringResult = response.Content;

            return JsonConvert.DeserializeObject<HiveOSWallet>(stringResult);
        }
    }

    public interface IHiveOSService
    {
        string GetAccessToken();
        List<HiveOSFarm> GetFarms();
        List<HiveOSWorker> GetWorkers(int farmId);
        List<HiveOSWallet> GetWallets(int farmId);
        HiveOSWallet GetWallet(int walletId);
    }

    public class HiveResult<T>
    {
        public T Data { get; set; }
    }
}


//request.AddJsonBody(new
//{
//    login = "Cyllon",
//    password = "Vannmelon1029.-",
//});

//request.AddBody(new
//{
//    api_key = "cannafarm_token_1111_xxx"
//});
