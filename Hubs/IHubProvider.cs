using Api.Chat.Model;

namespace Api.Chat.Hubs
{
    public interface IHubProvider
    {
        Task ReceiveCallerNotification(string message);
        Task ReceiveDirectMessage(string senderName, string message);
        Task ReceiveMessage(string user, string message);
        Task ReceiveFile(string user, string fileName);
        Task SaleMade(string message);
        Task SystemInstability(string message);
    }
}
