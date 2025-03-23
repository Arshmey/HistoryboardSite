namespace NewsSite.Models
{
    public class Message
    {

        public Message(string title, string content)
        {
            Title = title;
            Content = content;
            PublishDate = DateTime.UtcNow;
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
