using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteApi.Models
{
    public class BetResult
    {
        public string id { get; set; }
        public string idRoulette { get; set; }
        public string userId { get; set; }
        public string BetInformation { get; set; }
        public int Amount { get; set; }
        public string Result { get; set; }
        public string DateTimeResult { get; set;}
    }
}
