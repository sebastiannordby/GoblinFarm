using Newtonsoft.Json;

namespace App.Models.Fjordkraft
{
    public class FjordkraftResult
    {
        [JsonProperty("countryNo")]
        public int CountryNo { get; set; }

        [JsonProperty("postalLocation")]
        public string PostalLocation { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("priceAreaId")]
        public int PriceAreaId { get; set; }

        [JsonProperty("priceAreaName")]
        public string PriceAreaName { get; set; }
    }
}
