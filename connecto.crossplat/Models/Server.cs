using System.Collections.ObjectModel;

namespace connecto.crossplat.Models
{
    public class Server
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public ObservableCollection<Channel>? Channels { get; set; }
    }
} 