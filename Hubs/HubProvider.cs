﻿using Microsoft.AspNetCore.SignalR;

namespace Api.Chat.Hubs
{
    public class HubProvider : Hub
    {
        public async Task CallerNotification(string connectionId, string message)
        {
            await Clients.Client(connectionId).SendAsync("CallerNotification", message);
        }

        public async Task DirectMessage(string connectionId, string senderName, string message)
        {
            await Clients.Client(connectionId).SendAsync("DirectMessage", senderName, message);
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task UploadFile(string user, string fileName)
        {
            await Clients.All.SendAsync("ReceiveFile", user, fileName);
        }

        public async Task NotifySaleMade(string message)
        {
            await Clients.All.SendAsync("SaleMade", message);
        }

        public async Task NotifySystemInstability(string message)
        {
            await Clients.All.SendAsync("SystemInstability", message);
        }
    }
}
