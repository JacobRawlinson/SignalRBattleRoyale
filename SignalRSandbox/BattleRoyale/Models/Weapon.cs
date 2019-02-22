using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRSandbox.BattleRoyale.Models
{
    public class Weapon
    {
        public string Name { get; set; }
        public int HitRate { get; set; }
        public int Damage { get; set; }
        public WeaponSize Size { get; set; }

        public Weapon Clone()
        {
            return new Weapon()
            {
                Name = Name,
                HitRate = HitRate,
                Damage = Damage,
                Size = Size
            };
        }
    }
}