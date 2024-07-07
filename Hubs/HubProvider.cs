using Api.Chat.Model;
using Microsoft.AspNetCore.SignalR;

namespace Api.Chat.Hubs
{
    public class HubProvider : Hub<IHubProvider>
    {
        public async Task CallerNotification(string connectionId, string message)
        {
            await Clients.Client(connectionId).ReceiveCallerNotification(message);
        }

        public async Task DirectMessage(string connectionId, string senderName, string message)
        {
            await Clients.Client(connectionId).ReceiveDirectMessage(senderName, message);
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.ReceiveMessage(user, message);
        }

        public async Task UploadFile(string user, string fileName)
        {
            await Clients.All.ReceiveFile(user, fileName);
        }

        public async Task NotifySaleMade(string message)
        {
            await Clients.All.SaleMade(message);
        }

        public async Task NotifySystemInstability(string message)
        {
            await Clients.All.SystemInstability(message);
        }
    }
}
