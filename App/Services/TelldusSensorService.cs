using App.Classes;
using GoblinFarm.Models;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;

namespace GoblinFarm.Services
{
    // https://api.telldus.com/{format}/{function}
    //https://api.telldus.com/json/devices/list
    public class TelldusSensorService : ITelldusSensorService
    {
        private Uri baseUrl = new Uri(SystemVariables.TelldusSettings.ApiPrefix);

        private RestClient GetRestClient()
        {
            var client = new RestClient(baseUrl);

            client.Authenticator = OAuth1Authenticator.ForProtectedResource(
                SystemVariables.TelldusSettings.PublicKey, SystemVariables.TelldusSettings.PrivateKey,
                SystemVariables.TelldusSettings.Token, SystemVariables.TelldusSettings.TokenSecret);

            return client;
        }

        public TelldusClient GetClient()
        {
            var client = GetRestClient();
            var request = new RestRequest("json/devices/list", Method.GET);
            var response = client.Execute(request);

            string content = response.Content;

            return JsonConvert.DeserializeObject<TelldusClient>(content);
        }

        public TelldusClient GetSensors()
        {
            var client = GetRestClient();
            var request = new RestRequest("json/devices/list", Method.GET);
            var response = client.Execute(request);

            string content = response.Content;

            return JsonConvert.DeserializeObject<TelldusClient>(content);
        }

        public TelldusSensorList GetSensorList()
        {
            var client = GetRestClient();
            var request = new RestRequest("json/sensors/list", Method.GET);

            request.AddParameter("includeScale", 1, ParameterType.QueryString);
            request.AddParameter("includeValues", 1, ParameterType.QueryString);
            request.AddParameter("includeScale", 1, ParameterType.QueryString);
            request.AddParameter("includeUnit", 1, ParameterType.QueryString);

            var response = client.Execute(request);

            string content = response.Content;

            return JsonConvert.DeserializeObject<TelldusSensorList>(content);
        }
    }

    public interface ITelldusSensorService
    {
        TelldusSensorList GetSensorList();
        TelldusClient GetSensors();
        TelldusClient GetClient();
    }
}
