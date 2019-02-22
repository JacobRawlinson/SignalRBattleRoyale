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
        private List<string> players = new List<string>()
        {
            "Matt",
            "Leanne",
            "Sarmon",
            "Gideon",
            "Claire",
            "Will",
            "Gaz",
            "Pete",
            "Sean",
            "Kyle",
            "Walshy",
            "Andy H",
            "Jacob",
            "Chris",
            "Stacy"
        };

        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.

            if (message == "StartMatch")
            {
                GameHelper gh = new GameHelper();
                gh.Start(this, players);
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
    }
}