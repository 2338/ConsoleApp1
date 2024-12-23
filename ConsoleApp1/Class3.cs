using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoApp
{
    internal class Bet
    {
        public string ClientName { get; set; }
        public string GameName { get; set; }
        public decimal Amount { get; set; }
        public DateTime BetDate { get; set; }
    }
}
