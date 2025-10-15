using Microsoft.AspNet.SignalR;
using ProgramPartListWeb.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProgramPartListWeb.Utilities
{
    public class NotificationHub : Hub<IEmployee>
    {
        // Send a notification to all connected clients
        public void SendNotification(string message)
        {
            // Broadcast the notification message to all clients connected to the hub
            Clients.All.receiveNotification(message);
        }

        // Optionally, add methods to send messages to specific groups or users
        public void SendToGroup(string groupName, string message)
        {
            Clients.Group(groupName).receiveNotification(message);
        }

        // Called when a client connects
        public override Task OnConnected()
        {
            // You can add logic here if you need to track connections or
            // add them to groups dynamically, for instance.
            return base.OnConnected();
        }
    }
}