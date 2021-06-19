using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoblinFarm.Models.HiveOS
{
    public class HiveOSFarmStats
    {
        [JsonProperty("workers_total")]
        public int WorkersTotal { get; set; }

        [JsonProperty("workers_online")]
        public int WorkersOnline { get; set; }

        [JsonProperty("workers_offline")]
        public int WorkersOffline { get; set; }

        [JsonProperty("power_draw")]
        public decimal PowerDraw { get; set; }


    }

    public class HiveOSCoinHashrate
    {
        [JsonProperty("coin")]
        public string Coin { get; set; }

        [JsonProperty("Algo")]
        public string Algo { get; set; }

        [JsonProperty("hash_rate")]
        public int HashRate { get; set; } // kH/s
    }
}
