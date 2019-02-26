using SignalRSandbox.BattleRoyale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRSandbox.BattleRoyale.Helpers
{
    public class WeaponHelper
    {
        public List<Weapon> CreateWeapons(int amount)
        {
            var weapons = new List<Weapon>();
            var baseWeapons = GetBaseWeapons();

            var rnd = new Random();

            for (int i = 0; i < amount; i++)
            {
                var roll = rnd.Next(0, baseWeapons.Count());
                weapons.Add(baseWeapons[roll].Clone());
            }

            return weapons;
        }


        public List<Weapon> GetBaseWeapons()
        {
            var baseWeapons = new List<Weapon>();
            baseWeapons.Add(new Weapon() { Name = "Pencil", HitRate = 1, Damage = 1 });
            baseWeapons.Add(new Weapon() { Name = "Frying Pan", HitRate = 2, Damage = 4 });
            baseWeapons.Add(new Weapon() { Name = "Knife", HitRate = 4, Damage = 5 });
            baseWeapons.Add(new Weapon() { Name = "Pistol", HitRate = 5, Damage = 6 });
            baseWeapons.Add(new Weapon() { Name = "Machine Gun", HitRate = 7, Damage = 8 });
            baseWeapons.Add(new Weapon() { Name = "Sniper", HitRate = 8, Damage = 9 });
            baseWeapons.Add(new Weapon() { Name = "Hockey Stick", HitRate = 4, Damage = 4 });
            baseWeapons.Add(new Weapon() { Name = "Baseball Bat", HitRate = 5, Damage = 5 });
            baseWeapons.Add(new Weapon() { Name = "Giant Pink Dildo", HitRate = 2, Damage = 8 });
            baseWeapons.Add(new Weapon() { Name = "The 69er", HitRate = 3, Damage = 10 });

            return baseWeapons;
        }
    }
}