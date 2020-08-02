using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteApi.Models
{
    public class Bet
    {
        public string IdRoulette { get; set; }
        public string BetInformation { get; set; }
        public int Amount { get; set; }
    }
}
