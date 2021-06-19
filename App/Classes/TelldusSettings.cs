using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Classes
{
    public class TelldusSettings
    {
        public string ApiPrefix { get; set; }
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
        public string Token { get; set; }
        public string TokenSecret { get; set; }
    }
}
