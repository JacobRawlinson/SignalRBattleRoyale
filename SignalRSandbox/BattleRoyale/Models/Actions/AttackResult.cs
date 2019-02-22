using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRSandbox.BattleRoyale.Models.Actions
{
    public class AttackResult : ActionResult
    {
        public Weapon WeaponUsed { get; set; }
        public bool IsHit { get; set; }
        public bool IsKill { get; set; }
    }
}