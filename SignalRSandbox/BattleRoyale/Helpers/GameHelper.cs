using SignalRSandbox.BattleRoyale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRSandbox.BattleRoyale.Helpers
{
    public class GameHelper
    {
        private List<Player> players = new List<Player>();
        private Random rnd = new Random();
        private MessageHelper messageHelper = new MessageHelper();
        private ActionHelper actionHelper = new ActionHelper();
        private List<Weapon> weaponPool = new List<Weapon>();
        private ChatHub hub;
        
        public void Start(ChatHub chatHub, List<string> playerNames, Leaderboard leaderboard)
        {
            hub = chatHub;
            players = new List<Player>();

            foreach (var name in playerNames)
            {
                var player = new Player()
                {
                    Name = name,
                    IsBot = name.StartsWith("Bot")
                };
                players.Add(player);
                var newPlayer = new PlayerStats { Player = name };

                if (!player.IsBot)
                {
                    leaderboard.LeaderboardEntries.Add(newPlayer);
                }
            }

            var WeaponHelper = new WeaponHelper();

            weaponPool = WeaponHelper.CreateWeapons(players.Count);

            hub.Broadcast(messageHelper.Begin(players));
            hub.Broadcast("--------------------------------------------------");
            LootingPhase();
            WarPhase();

            var winner = players.First(p => p.State != PlayerState.Dead);
            if (!winner.IsBot)
            {
                var leaderboardEntry = leaderboard.LeaderboardEntries.Where(x => x.Player == winner.Name).FirstOrDefault();
                leaderboardEntry.Wins++;
                leaderboardEntry.TotalKills += winner.KillCount;
            }
            hub.Broadcast("--------------------------------------------------");
            hub.Broadcast(messageHelper.PlayerWins(winner));
            hub.Broadcast("--------------------------------------------------");

            var kills = messageHelper.GetPlayersKilled(players);

            foreach(var kill in kills)
            {
                hub.Broadcast(kill);
            }
        }

        private void LootingPhase()
        {
            while (GetUnarmedPlayers().Any())
            {
                var unarmedPlayers = GetUnarmedPlayers();
                var armedPlayers = GetArmedPlayers();

                var isAttack = armedPlayers.Any() && rnd.Next(0, 11) == 10;

                if (isAttack)
                {
                    var playerIndex = rnd.Next(0, armedPlayers.Count());
                    var actingPlayer = armedPlayers[playerIndex];

                    var targets = GetTargetsForPlayer(actingPlayer);
                    var targetPlayerIndex = rnd.Next(0, targets.Count());
                    var target = targets[targetPlayerIndex];

                    var attack = actionHelper.AttackAction(actingPlayer, target);

                    hub.Broadcast(messageHelper.GetMessageForAction(attack));

                    if (attack.IsKill)
                    {
                        actingPlayer.Kills.Add(attack);
                    }
                }
                else
                {
                    var playerIndex = rnd.Next(0, unarmedPlayers.Count());
                    var actingPlayer = unarmedPlayers[playerIndex];

                    var weaponIndex = rnd.Next(0, weaponPool.Count());
                    var targetWeapon = weaponPool[weaponIndex];

                    var loot = actionHelper.LootAction(actingPlayer, null, targetWeapon);
                    weaponPool.RemoveAt(weaponIndex);

                    hub.Broadcast(messageHelper.GetMessageForAction(loot));
                }

                System.Threading.Thread.Sleep(2000);
            }
        }

        private void WarPhase()
        {
            while (GetAlivePlayers().Count() != 1)
            {
                var alivePlayers = GetAlivePlayers();
                var deadPlayers = GetDeadPlayers();

                var playerIndex = rnd.Next(0, alivePlayers.Count());
                var actingPlayer = alivePlayers[playerIndex];

                var diceRoll = rnd.Next(1, 10);

                if (diceRoll <= 6)
                {
                    var targets = GetTargetsForPlayer(actingPlayer);
                    var targetPlayerIndex = rnd.Next(0, targets.Count());
                    var target = targets[targetPlayerIndex];

                    var attack = actionHelper.AttackAction(actingPlayer, target);

                    hub.Broadcast(messageHelper.GetMessageForAction(attack));

                    if (attack.IsKill)
                    {
                        actingPlayer.Kills.Add(attack);
                        if (actingPlayer.Weapon.Damage < target.Weapon.Damage)
                        {
                            var loot = actionHelper.LootAction(actingPlayer, target, target.Weapon);
                            hub.Broadcast(messageHelper.GetMessageForAction(loot));
                        }
                    }
                }
                else if (diceRoll <= 7 && deadPlayers.Any())
                {
                    var targetPlayerIndex = rnd.Next(0, deadPlayers.Count());
                    var target = deadPlayers[targetPlayerIndex];

                    var gouged = actionHelper.GougedAction(actingPlayer, target);

                    hub.Broadcast(messageHelper.GetMessageForAction(gouged));
                }
                else if (diceRoll <= 8 && deadPlayers.Any())
                {
                    var targetPlayerIndex = rnd.Next(0, deadPlayers.Count());
                    var target = deadPlayers[targetPlayerIndex];

                    var teabag = actionHelper.TeabagAction(actingPlayer, target);

                    hub.Broadcast(messageHelper.GetMessageForAction(teabag));
                }
                else if (diceRoll <= 9 && deadPlayers.Any())
                {
                    var targetPlayerIndex = rnd.Next(0, deadPlayers.Count());
                    var target = deadPlayers[targetPlayerIndex];

                    var teabag = actionHelper.ViolateAction(actingPlayer, target);

                    hub.Broadcast(messageHelper.GetMessageForAction(teabag));
                }
                else if (diceRoll <= 10)
                {
                    var cower = actionHelper.CowerAction(actingPlayer);

                    hub.Broadcast(messageHelper.GetMessageForAction(cower));
                }

                System.Threading.Thread.Sleep(3000);
            }
        }

        private List<Player> GetAlivePlayers()
        {
            return players.Where(p => p.State != PlayerState.Dead).ToList();
        }

        private List<Player> GetDeadPlayers()
        {
            return players.Where(p => p.State == PlayerState.Dead).ToList();
        }

        private List<Player> GetUnarmedPlayers()
        {
            return GetAlivePlayers().Where(p => p.Weapon == null).ToList();
        }

        private List<Player> GetArmedPlayers()
        {
            return GetAlivePlayers().Where(p => p.Weapon != null).ToList();
        }

        public List<Player> GetTargetsForPlayer(Player player)
        {
            return GetAlivePlayers().Where(p => p != player).ToList();
        }
    }
}