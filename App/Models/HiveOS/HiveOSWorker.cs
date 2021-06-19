using GoblinFarm.Models.HiveOS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GoblinFarm.Models
{
    public class HiveOSWorker
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("needs_upgrade")]
        public bool NeedUpgrade { get; set; }

        [JsonProperty("powermeter_stats")]
        public HiveOSPowermeterStats PowermeterStats { get; set; }

        [JsonProperty("flight_sheet")]
        public HiveOSWorkerFlightSheet FlightSheet { get; set; }

        public class HiveOSWorkerFlightSheet
        {
            [JsonProperty("id")]
            public int Id { get; set; }
            
            [JsonProperty("farm_id")]
            public int FarmId { get; set; }

            [JsonProperty("pool")]
            public string Pool { get; set; }

            [JsonProperty("items")]
            public List<FlightSheetItems> Items { get; set; }

            public class FlightSheetItems
            {
                [JsonProperty("coin")]
                public string Coin { get; set; }

                [JsonProperty("pool")]
                public string Pool { get; set; }

                [JsonProperty("wal_id")]
                public int WalletId { get; set; }
            }
        }
    }

}
