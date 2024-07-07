namespace api_chat.Model
{
    public class DirectMessageRequest
    {
        public string TargetConnectionId { get; set; }
        public string Message { get; set; }
        public string SenderName { get; set; }
    }
}
