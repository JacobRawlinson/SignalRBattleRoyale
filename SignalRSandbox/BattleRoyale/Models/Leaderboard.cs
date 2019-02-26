using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRSandbox.BattleRoyale.Models
{
    public class Leaderboard
    {
        public HashSet<PlayerStats> LeaderboardEntries { get; set; }
    }
}