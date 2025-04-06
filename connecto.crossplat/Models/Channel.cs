using System.Collections.ObjectModel;

namespace connecto.crossplat.Models
{
    public class Channel
    {
        public int Id { get; set; }
        public int ServerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public ObservableCollection<Message>? Messages { get; set; }
    }
} 