using App.Models.Fjordkraft;
using Newtonsoft.Json;
using RestSharp;

namespace App.Services
{
    public class FjordkraftService : IFjordkraftService
    {
        public FjordkraftResult GetElectricityPrice(string postalCode)
        {
            var client = new RestClient("https://rest.fjordkraft.no/pricecalculator/priceareainfo/private/");
            var request = new RestRequest(postalCode, Method.GET);

            request.AddHeader("Content-Type", "application/json");
            request.RequestFormat = DataFormat.Json;

            var response = client.Execute(request);
            var stringResult = response.Content;

            return JsonConvert.DeserializeObject<FjordkraftResult>(stringResult);
        }
    }

    public interface IFjordkraftService
    {
        FjordkraftResult GetElectricityPrice(string postalCode);
    }
}
