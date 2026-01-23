namespace ZABCareersAPIs.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string MessageText { get; set; }
        public string CheckStatus { get; set; }
        public int MessageStatus { get; set; }
    }
}
