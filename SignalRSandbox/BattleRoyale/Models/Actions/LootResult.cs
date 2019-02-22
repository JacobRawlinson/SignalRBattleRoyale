using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRSandbox.BattleRoyale.Models.Actions
{
    public class LootResult : ActionResult
    {
        public Weapon TargetItem { get; set; }
    }
}