using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Classes
{
    public static class SystemVariables
    {
        public static TelldusSettings TelldusSettings { get; set; } = new TelldusSettings();
        public static HiveOSSettings HiveOSSettings { get; set; } = new HiveOSSettings();
        public static CoinbaseSettings CoinbaseSettings { get; set; } = new CoinbaseSettings();
        public static List<string> SupportedCoins { get; set; } = new List<string>();
    }
}
