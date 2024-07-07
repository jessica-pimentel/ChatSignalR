using Api.Chat.Hubs;
using api_chat.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace api_chat.Controllers
{
    [Route("hub")]
    [ApiController]
    public class HubController : ControllerBase
    {
        private readonly IHubContext<HubProvider> _hubContext;
        private readonly ILogger<HubController> _logger;

        public HubController(IHubContext<HubProvider> hubContext, ILogger<HubController> logger)
        {
            _hubContext = hubContext;
            _logger = logger;
        }

        [HttpPost("notifyCaller")]
        public async Task<IActionResult> NotifyCaller([FromBody] dynamic data)
        {
            try
            {
                string connectionId = data.connectionId;
                string message = data.message;

                _logger.LogInformation("Enviando notificação para o cliente: ConnectionId = {ConnectionId}, Message = {Message}", connectionId, message);

                await _hubContext.Clients.Client(connectionId).SendAsync("ReceiveCallerNotification", message);

                return Ok(new
                {
                    Message = "Notificação para o cliente que chamou enviada com sucesso!",
                    NotificationType = "ReceiveCallerNotification",
                    ConnectionId = connectionId
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao enviar notificação para o cliente");
                return StatusCode(500, "Erro ao enviar notificação para o cliente");
            }
        }

        [HttpPost("sendDirectMessage")]
        public async Task<IActionResult> SendDirectMessage([FromBody] DirectMessageRequest request)
        {
            try
            {
                string targetConnectionId = request.TargetConnectionId;
                string message = request.Message;
                string senderName = request.SenderName;

                _logger.LogInformation("Enviando mensagem direta: TargetConnectionId = {TargetConnectionId}, Message = {Message}, SenderName = {SenderName}", targetConnectionId, message, senderName);

                await _hubContext.Clients.Client(targetConnectionId).SendAsync("ReceiveDirectMessage", senderName, message);

                return Ok(new
                {
                    Message = "Mensagem enviada com sucesso!",
                    TargetConnectionId = targetConnectionId,
                    SentMessage = message,
                    SenderName = senderName
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao enviar mensagem direta");
                return StatusCode(500, "Erro ao enviar mensagem direta");
            }
        }

        [HttpPost("sendMessage")]
        public async Task<IActionResult> SendMessage([FromBody] dynamic data)
        {
            try
            {
                string user = data.user;
                string message = data.message;

                _logger.LogInformation("Enviando mensagem: User = {User}, Message = {Message}", user, message);

                await _hubContext.Clients.All.SendAsync("ReceiveMessage", user, message);

                return Ok(new
                {
                    Message = "Mensagem enviada com sucesso!",
                    User = user,
                    SentMessage = message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao enviar mensagem");
                return StatusCode(500, "Erro ao enviar mensagem");
            }
        }

        [HttpPost("uploadFile")]
        public async Task<IActionResult> UploadFile([FromBody] dynamic data)
        {
            try
            {
                string user = data.user;
                string fileName = data.fileName;

                _logger.LogInformation("Enviando arquivo: User = {User}, FileName = {FileName}", user, fileName);

                await _hubContext.Clients.All.SendAsync("ReceiveFile", user, fileName);

                return Ok(new
                {
                    Message = "Arquivo enviado com sucesso!",
                    User = user,
                    FileName = fileName
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao enviar arquivo");
                return StatusCode(500, "Erro ao enviar arquivo");
            }
        }

        [HttpPost("notifySaleMade")]
        public async Task<IActionResult> NotifySaleMade([FromBody] dynamic data)
        {
            try
            {
                string message = data.message;

                _logger.LogInformation("Notificando venda realizada: Message = {Message}", message);

                await _hubContext.Clients.All.SendAsync("SaleMade", message);

                return Ok(new
                {
                    Message = "Notificação de venda feita enviada com sucesso!",
                    SaleMessage = message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao notificar venda realizada");
                return StatusCode(500, "Erro ao notificar venda realizada");
            }
        }

        [HttpPost("notifySystemInstability")]
        public async Task<IActionResult> NotifySystemInstability([FromBody] dynamic data)
        {
            try
            {
                string message = data.message;

                _logger.LogInformation("Notificando instabilidade do sistema: Message = {Message}", message);

                await _hubContext.Clients.All.SendAsync("SystemInstability", message);

                return Ok(new
                {
                    Message = "Notificação de instabilidade do sistema enviada com sucesso!",
                    InstabilityMessage = message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao notificar instabilidade do sistema");
                return StatusCode(500, "Erro ao notificar instabilidade do sistema");
            }
        }
    }
}
