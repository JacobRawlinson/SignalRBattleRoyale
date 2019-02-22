using SignalRSandbox.BattleRoyale.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRSandbox.BattleRoyale.Models.Actions
{
    public class ActionResult
    {
        public Player ActingPlayer { get; set; }
        public Player TargetPlayer { get; set; }
        public PlayerAction Action { get; set; }
    }
}