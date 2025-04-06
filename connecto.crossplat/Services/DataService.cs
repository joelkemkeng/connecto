using System.Collections.ObjectModel;
using System.Linq;
using connecto.crossplat.Models;

namespace connecto.crossplat.Services
{
    public class DataService
    {
        public ObservableCollection<Server> Servers { get; private set; } = new();
        public ObservableCollection<Channel> Channels { get; private set; } = new();
        public ObservableCollection<Message> Messages { get; private set; } = new();

        public DataService()
        {
            InitializeDemoData();
        }

        private void InitializeDemoData()
        {
            // Initialiser les serveurs
            Servers.Clear();
            foreach (var server in new[]
            {
                new Server { Id = 1, Name = "Général", Icon = "🏠" },
                new Server { Id = 2, Name = "Projets", Icon = "📊" },
                new Server { Id = 3, Name = "Marketing", Icon = "📣" },
                new Server { Id = 4, Name = "Design", Icon = "🎨" }
            })
            {
                Servers.Add(server);
            }

            // Initialiser les canaux
            Channels.Clear();
            foreach (var channel in new[]
            {
                new Channel { Id = 1, ServerId = 1, Name = "Général", Icon = "# " },
                new Channel { Id = 2, ServerId = 1, Name = "Annonces", Icon = "# " },
                new Channel { Id = 3, ServerId = 1, Name = "Questions", Icon = "# " }
            })
            {
                Channels.Add(channel);
            }

            // Initialiser les messages
            Messages.Clear();
            foreach (var message in new[]
            {
                new Message 
                { 
                    Id = 1, 
                    ChannelId = 1, 
                    Username = "System", 
                    Content = "Bienvenue sur Connecto!", 
                    Timestamp = "Aujourd'hui à 10:00", 
                    AvatarUrl = "/Assets/logo.png" 
                },
                new Message 
                { 
                    Id = 2, 
                    ChannelId = 1, 
                    Username = "Admin", 
                    Content = "Ceci est une démonstration de l'interface", 
                    Timestamp = "Aujourd'hui à 10:05", 
                    AvatarUrl = "/Assets/logo.png" 
                },
                new Message 
                { 
                    Id = 3, 
                    ChannelId = 1, 
                    Username = "User1", 
                    Content = "Super application !", 
                    Timestamp = "Aujourd'hui à 10:10", 
                    AvatarUrl = "/Assets/logo.png" 
                }
            })
            {
                Messages.Add(message);
            }

            // Établir les relations
            foreach (var server in Servers)
            {
                server.Channels = new ObservableCollection<Channel>(
                    Channels.Where(c => c.ServerId == server.Id)
                );
            }

            foreach (var channel in Channels)
            {
                channel.Messages = new ObservableCollection<Message>(
                    Messages.Where(m => m.ChannelId == channel.Id)
                );
            }
        }

        public void AddMessage(int channelId, string username, string content)
        {
            var newMessage = new Message
            {
                Id = Messages.Count + 1,
                ChannelId = channelId,
                Username = username,
                Content = content,
                Timestamp = "À l'instant",
                AvatarUrl = "/Assets/logo.png"
            };

            Messages.Add(newMessage);

            var channel = Channels.FirstOrDefault(c => c.Id == channelId);
            channel?.Messages?.Add(newMessage);
        }

        public ObservableCollection<Channel> GetChannelsForServer(int serverId)
        {
            return new ObservableCollection<Channel>(Channels.Where(c => c.ServerId == serverId));
        }

        public ObservableCollection<Message> GetMessagesForChannel(int channelId)
        {
            return new ObservableCollection<Message>(Messages.Where(m => m.ChannelId == channelId));
        }
    }
} 