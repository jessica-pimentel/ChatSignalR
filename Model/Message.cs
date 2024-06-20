namespace Api.Chat.Model
{
    public class Message
    {
        public string Name { get; set; }
        public string Body { get; set; }
        public string Date { get; set; }

        public Message(string name, string body)
        {
            Name = name;
            Body = body;
            Date = DateTime.Now.ToString("G");
        }


    }
}
