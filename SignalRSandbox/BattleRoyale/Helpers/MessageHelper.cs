using SignalRSandbox.BattleRoyale.Models;
using SignalRSandbox.BattleRoyale.Models.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRSandbox.BattleRoyale.Helpers
{
    public class MessageHelper
    {
        public string GetMessageForAction(ActionResult result)
        {
            var message = string.Empty;

            if (result is AttackResult attack)
            {
                message = $"{attack.ActingPlayer.Name} attacked {attack.TargetPlayer.Name} with {attack.WeaponUsed.Name}.";

                if (attack.IsHit)
                {
                    message += " The attack hit!";
                    if (attack.IsKill)
                    {
                        message += $" {attack.TargetPlayer.Name} is dead.";
                    }
                    else
                    {
                        message += $" {attack.TargetPlayer.Name} is bleeding, but still alive.";
                    }
                }
                else
                {
                    message += " The attack missed.";
                }
            }
            else if (result is LootResult loot)
            {
                message = $"{loot.ActingPlayer.Name} picked up a {loot.TargetItem.Name}";

                if (loot.TargetPlayer != null)
                {
                    message += $" from {loot.TargetPlayer.Name}'s bloody remains";
                }
            }
            else
            {
                switch (result.Action)
                {
                    case PlayerAction.Teabag:
                        message = $"{result.ActingPlayer.Name} proceeds to teabag {result.TargetPlayer.Name}'s corpse";
                        break;
                    case PlayerAction.Cower:
                        message = $"{result.ActingPlayer.Name} cowers in fear";
                        break;
                    case PlayerAction.Violated:
                        message = $"{result.ActingPlayer.Name} violates {result.TargetPlayer.Name}'s corpse with {result.ActingPlayer.Weapon.Name}";
                        break;
                    case PlayerAction.Gouged:
                        message = $"{result.ActingPlayer.Name} gouged {result.TargetPlayer.Name}'s eyes out with a spoon";
                        break;
                    default:
                        message = $"{result.ActingPlayer.Name} gorms into the sky";
                        break;
                }
            }

            return message;
        }

        public string Begin(List<Player> players)
        {
            var playerString = string.Join(", ", players.Select(x => x.Name));
            return $"The battle has begun! {playerString} all scramble to grab a weapon";
        }

        public string PlayerWins(Player player)
        {
            return $"{player.Name} has won the Battle Royale with {player.KillCount} kills, I hope you're happy with yourself";
        }

        public List<string> GetPlayersKilled(List<Player> players)
        {
            var message = new List<string>();

            foreach(var player in players)
            {
                message.Add($"{player.Name} killed {player.KillCount} players");

                foreach (var kill in player.Kills)
                {
                    message.Add($"{player.Name} killed {kill.TargetPlayer.Name} with a {kill.WeaponUsed.Name}");
                }
                message.Add("");

            }


            return message;
        }
    }
}