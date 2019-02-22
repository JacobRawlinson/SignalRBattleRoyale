using SignalRSandbox.BattleRoyale.Models.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRSandbox.BattleRoyale.Models
{
    public class Player
    {
        public string Name { get; set; }
        public PlayerState State { get; set; }
        public Weapon Weapon { get; set; }
        public List<AttackResult> Kills { get; set; } = new List<AttackResult>();
        public int KillCount => Kills.Count();
     }
}