using Api.Chat.Model;

namespace Api.Chat.Hubs
{
    public interface IHubProvider
    {
        Task ReceiveCallerNotification(string message);
        Task ReceiveDirectMessage(string senderName, string message);
    }
}
