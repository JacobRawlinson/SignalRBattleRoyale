using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using SignalRSandbox.BattleRoyale.Helpers;

namespace SignalRSandbox
{
    public class ChatHub : Hub
    {
        private List<string> players = new List<string>();

        public void Send(string name, string message)
        {
            if (message.ToLower() == "/startmatch")
            {
                GameHelper gh = new GameHelper();
                gh.Start(this, players);
                players = new List<string>();
            }
            else if (message.ToLower() == "/whois")
            {
                Broadcast($"Listing Players");

                foreach (var player in players)
                {
                    Broadcast($"{player} is participating");
                }
            }
            else if (message.ToLower() == "/join")
            {
                AddPlayer(name);
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

        private void AddPlayer(string name)
        {
            players.Add(name);
            Broadcast($"{name} has joined the battle royale");
        }

        public void JoinChat(string name)
        {
            Clients.All.broadcastMessage(name + " is viewing the match");
        }
    }
}