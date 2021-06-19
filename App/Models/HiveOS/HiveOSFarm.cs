using Newtonsoft.Json;

namespace GoblinFarm.Models
{
    public class HiveOSFarm
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("timezone")]
        public string TimeZone { get; set; }

        [JsonProperty("workers_count")]
        public int WorkersCount { get; set; }
    }
}
