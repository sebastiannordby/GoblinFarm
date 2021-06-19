using System;

namespace GoblinFarm.EF
{
    public class TelldusAccumulatedSensorLog
    {
        public Guid Id { get; set; }
        public decimal DataValue { get; set; }
        public string DataUnit { get; set; }
        public decimal ElectricityCost_KwH_Ore { get; set; }
    }
}
