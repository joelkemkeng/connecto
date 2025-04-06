namespace connecto.crossplat.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int ChannelId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Timestamp { get; set; } = string.Empty;
        public string AvatarUrl { get; set; } = string.Empty;
    }
} 