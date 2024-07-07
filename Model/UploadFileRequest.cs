namespace api_chat.Model
{
    public class UploadFileRequest
    {
        public string User { get; set; }
        public IFormFile File { get; set; }
    }
}
