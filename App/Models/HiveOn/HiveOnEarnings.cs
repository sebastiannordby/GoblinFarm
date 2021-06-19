using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace App.Models.HiveOn
{
    public class HiveOnEarnings
    {
        [JsonProperty("EarningsStats")]
        public List<HiveOnEarnings> EarningsStats { get; set; }

        [JsonProperty("succeedPayouts")]
        public List<HiveOnPayout> SucceedPayouts { get; set; }

        [JsonProperty("expectedReward24H")]
        public decimal ExpectedReward24H { get; set; }

        [JsonProperty("expectedRewardWeek")]
        public decimal ExpectedRewardWeek { get; set; }

        [JsonProperty("totalPaid")]
        public decimal TotalPaid { get; set; }

        [JsonProperty("totalUnpaid")]
        public decimal TotalUnpaid { get; set; }
    }

    public class HiveOnPayout
    {
        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("approveUUID")]
        public Guid ApproveUUID { get; set; }

        [JsonProperty("coin")]
        public string Coin { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class HiveOnEarningsStats
    {
        [JsonProperty("meanReward")]
        public decimal MeanReward { get; set; }

        [JsonProperty("reward")]
        public decimal Reward { get; set; }

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
