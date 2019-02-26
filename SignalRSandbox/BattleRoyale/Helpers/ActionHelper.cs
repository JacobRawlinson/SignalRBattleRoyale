using SignalRSandbox.BattleRoyale.Models;
using SignalRSandbox.BattleRoyale.Models.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRSandbox.BattleRoyale.Helpers
{
    public class ActionHelper
    {
        public AttackResult AttackAction(Player actingPlayer, Player targetPlayer)
        {
            var attackResult = new AttackResult()
            {
                ActingPlayer = actingPlayer,
                TargetPlayer = targetPlayer,
                WeaponUsed = actingPlayer.Weapon,
                Action = PlayerAction.Attack
            };

            Random rnd = new Random();
            int hitRoll = rnd.Next(1, 11);

            if (hitRoll <= actingPlayer.Weapon.HitRate)
            {
                attackResult.IsHit = true;
                targetPlayer.State = PlayerState.Injured;

                int damageRoll = rnd.Next(1, 11);
                if (damageRoll <= actingPlayer.Weapon.Damage)
                {
                    attackResult.IsKill = true;
                    targetPlayer.State = PlayerState.Dead;
                }
            }

            return attackResult;
        }

        public LootResult LootAction(Player actingPlayer, Player targetPlayer, Weapon targetItem)
        {
            var lootResult = new LootResult()
            {
                ActingPlayer = actingPlayer,
                TargetPlayer = targetPlayer,
                TargetItem = targetItem,
                Action = PlayerAction.Loot
            };

            actingPlayer.Weapon = targetItem;

            return lootResult;
        }

        public ActionResult TeabagAction(Player actingPlayer, Player targetPlayer)
        {
            return new ActionResult()
            {
                ActingPlayer = actingPlayer,
                TargetPlayer = targetPlayer,
                Action = PlayerAction.Teabag
            };
        }

        public ActionResult CowerAction(Player actingPlayer)
        {
            return new ActionResult()
            {
                ActingPlayer = actingPlayer,
                Action = PlayerAction.Cower
            };
        }

        public ActionResult ViolateAction(Player actingPlayer, Player targetPlayer)
        {
            return new ActionResult()
            {
                ActingPlayer = actingPlayer,
                TargetPlayer = targetPlayer,
                Action = PlayerAction.Violated
            };
        }

        public ActionResult GougedAction(Player actingPlayer, Player targetPlayer)
        {
            return new ActionResult()
            {
                ActingPlayer = actingPlayer,
                TargetPlayer = targetPlayer,
                Action = PlayerAction.Gouged
            };
        }
    }
}