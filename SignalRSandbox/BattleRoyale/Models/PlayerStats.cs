using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRSandbox.BattleRoyale.Models
{
    public class PlayerStats
    {
        public string Player { get; set; }
        public int Wins { get; set; }
        public int TotalKills { get; set; }
    }
}