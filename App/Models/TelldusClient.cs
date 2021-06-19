using System.Collections.Generic;

namespace GoblinFarm.Models
{
    public class TelldusClient
    {
        public List<TelldusDevice> Device { get; set; }

        public class TelldusDevice
        {
            public string Id { get; set; }
            public string ClientDeviceId { get; set; }
            public string State { get; set; }
            public string ClientName { get; set; }
            public string Client { get; set; }
            public string Online { get; set; }
            public int Editable { get; set; }
        }
    }
}
