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
    }
}
