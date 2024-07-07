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
        public async Task<IActionResult> NotifyCaller([FromBody] NotifyCallerRequest request)
        {
            try
            {
                _logger.LogInformation("Enviando notificação para o cliente: ConnectionId = {ConnectionId}, Message = {Message}", request.ConnectionId, request.Message);

                await _hubContext.Clients.Client(request.ConnectionId).SendAsync("CallerNotification", request.Message);

                return Ok(new
                {
                    Message = "Notificação para o cliente que chamou enviada com sucesso!",
                    NotificationType = "CallerNotification",
                    ConnectionId = request.ConnectionId
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
                _logger.LogInformation("Enviando mensagem direta: TargetConnectionId = {TargetConnectionId}, Message = {Message}, SenderName = {SenderName}", request.TargetConnectionId, request.Message, request.SenderName);

                await _hubContext.Clients.Client(request.TargetConnectionId).SendAsync("DirectMessage", request.SenderName, request.Message);

                return Ok(new
                {
                    Message = "Mensagem enviada com sucesso!",
                    TargetConnectionId = request.TargetConnectionId,
                    SentMessage = request.Message,
                    SenderName = request.SenderName
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao enviar mensagem direta");
                return StatusCode(500, "Erro ao enviar mensagem direta");
            }
        }

        [HttpPost("sendMessage")]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageRequest request)
        {
            try
            {
                _logger.LogInformation("Enviando mensagem: User = {User}, Message = {Message}", request.User, request.Message);

                await _hubContext.Clients.All.SendAsync("ReceiveMessage", request.User, request.Message);

                return Ok(new
                {
                    Message = "Mensagem enviada com sucesso!",
                    User = request.User,
                    SentMessage = request.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao enviar mensagem");
                return StatusCode(500, "Erro ao enviar mensagem");
            }
        }

        [HttpPost("uploadFile")]
        public async Task<IActionResult> UploadFile([FromBody] UploadFileRequest request)
        {
            try
            {
                _logger.LogInformation("Enviando arquivo: User = {User}, FileName = {FileName}", request.User, request.FileName);

                await _hubContext.Clients.All.SendAsync("ReceiveFile", request.User, request.FileName);

                return Ok(new
                {
                    Message = "Arquivo enviado com sucesso!",
                    User = request.User,
                    FileName = request.FileName
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao enviar arquivo");
                return StatusCode(500, "Erro ao enviar arquivo");
            }
        }

        [HttpPost("notifySaleMade")]
        public async Task<IActionResult> NotifySaleMade([FromBody] NotifySaleMadeRequest request)
        {
            try
            {
                _logger.LogInformation("Notificando venda realizada: Message = {Message}", request.Message);

                await _hubContext.Clients.All.SendAsync("SaleMade", request.Message);

                return Ok(new
                {
                    Message = "Notificação de venda feita enviada com sucesso!",
                    SaleMessage = request.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao notificar venda realizada");
                return StatusCode(500, "Erro ao notificar venda realizada");
            }
        }

        [HttpPost("notifySystemInstability")]
        public async Task<IActionResult> NotifySystemInstability([FromBody] NotifySystemInstabilityRequest request)
        {
            try
            {
                _logger.LogInformation("Notificando instabilidade do sistema: Message = {Message}", request.Message);

                await _hubContext.Clients.All.SendAsync("SystemInstability", request.Message);

                return Ok(new
                {
                    Message = "Notificação de instabilidade do sistema enviada com sucesso!",
                    InstabilityMessage = request.Message
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
