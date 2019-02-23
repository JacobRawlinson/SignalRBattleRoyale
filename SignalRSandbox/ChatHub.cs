using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using SignalRSandbox.BattleRoyale.Helpers;
using SignalRSandbox.BattleRoyale.Models;

namespace SignalRSandbox
{
    public class ChatHub : Hub
    {
        public static HashSet<string> players = new HashSet<string>();
        public static Leaderboard leaderboard = new Leaderboard { LeaderboardEntries = new HashSet<PlayerStats>() };
        //{
        //    "Matt",
        //    "Leanne",
        //    "Sarmon",
        //    "Gideon",
        //    "Claire",
        //    "Will",
        //    "Gaz",
        //    "Pete",
        //    "Sean",
        //    "Kyle",
        //    "Walshy",
        //    "Andy H",
        //    "Jacob",
        //    "Chris",
        //    "Stacy"
        //};

        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.

            if (message == "StartMatch")
            {
                var botNumber = 1; //In case there are not enough players
                GameHelper gh = new GameHelper();
                
                while (players.Count < 5)
                {
                    
                    players.Add($"Bot {botNumber}");
                    botNumber++;
                }

                gh.Start(this, players.ToList(), leaderboard);
            }
            else
            {
                Broadcast($"{name} says: {message}");
            }
        }

        public void Broadcast(string message)
        {
            Clients.All.broadcastMessage(message);
        }

        public void JoinChat(string name)
        {
            Clients.All.broadcastMessage(name + " is viewing the match");
        }

        public void AddPlayer(string name)
        {
            players.Add(name);
        }
    }
}