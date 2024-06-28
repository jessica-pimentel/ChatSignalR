using Api.Chat.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace api_chat.Controllers
{
    [Route("hub")]
    public class HubController : ControllerBase
    {
        private readonly IHubContext<HubProvider> _hubContext;
        private readonly IHubProvider _hubProvider;

        public HubController(IHubContext<HubProvider> hubContext,
                             IHubProvider hubProvider)
        {
            _hubContext = hubContext;
            _hubProvider = hubProvider;
        }

        [HttpPost("notifyCaller")]
        public async Task<IActionResult> NotifyCaller([FromBody] dynamic data)
        {
            string connectionId = data.connectionId;

            await _hubContext.Clients.Client(connectionId).SendAsync("CallerNotification", "Notificação apenas para o cliente que chamou.");
            return Ok(new
            {
                Message = "Notificação para o cliente que chamou enviada com sucesso!",
                NotificationType = "CallerNotification",
                ConnectionId = connectionId
            });
        }

        [HttpPost("sendDirectMessage")]
        public async Task<IActionResult> SendDirectMessage([FromBody] dynamic data)
        {
            string targetConnectionId = data.targetConnectionId;
            string message = data.message;
            string senderName = data.senderName;

            await _hubContext.Clients.Client(targetConnectionId).SendAsync("DirectMessage", senderName, message);
            return Ok(new
            {
                Message = "Mensagem enviada com sucesso!",
                TargetConnectionId = targetConnectionId,
                SentMessage = message,
                SenderName = senderName
            });
        }
    }
}
