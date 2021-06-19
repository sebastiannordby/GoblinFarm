using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace GoblinFarm.Models
{
    public class TelldusSensorList
    {
        public List<TelldusSensor> Sensor { get; set; }
    }

    public class TelldusSensor
    {
        public string Id { get; set; }
        public string SensorId { get; set; }
        public string ClientName { get; set; }
        public string Name { get; set; }
        public string Protocol { get; set; }
        public long LastUpdated { get; set; }

        public List<TelldusSensorData> Data { get; set; }

        public class TelldusSensorData
        {
            public string Name { get; set; }
            public decimal Value { get; set; }
            public int Scale { get; set; }
            public string Unit { get; set; }
            public long LastUpdated { get; set; }
        }
    }
}
