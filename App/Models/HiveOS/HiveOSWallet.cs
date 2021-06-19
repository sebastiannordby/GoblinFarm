using Newtonsoft.Json;

namespace GoblinFarm.Models.HiveOS
{
    public class HiveOSWallet
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("coin")]
        public string Coin { get; set; }

        [JsonProperty("wal")]
        public string Wal { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("balance")]
        public WalletBalance Balance { get; set; }

        public class WalletBalance
        {
            [JsonProperty("description")]
            public string Description { get; set; }

            [JsonProperty("value")]
            public decimal Value { get; set; }

            [JsonProperty("value_fiat")]
            public decimal ValueFiat { get; set; }

        }
    }
}
