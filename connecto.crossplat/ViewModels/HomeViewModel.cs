using System;
using System.Collections.ObjectModel;
using System.Linq; // Ajout de cette directive pour les méthodes LINQ
using Avalonia;
using Avalonia.Controls;
using Avalonia.VisualTree;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace connecto.crossplat.ViewModels
{
    public partial class HomeViewModel : ViewModelBase
    {
        private const double COLLAPSE_WIDTH_THRESHOLD = 1000; // Largeur en pixels en dessous de laquelle la barre se réduit
        private Window? _window;

        [ObservableProperty]
        private string _currentUser = "Admin";

        [ObservableProperty]
        private ObservableCollection<ServerModel> _servers = new(); // Initialisation ici

        [ObservableProperty]
        private ObservableCollection<ChannelModel> _channels = new(); // Initialisation ici

        [ObservableProperty]
        private ObservableCollection<MessageModel> _messages = new(); // Initialisation ici

        [ObservableProperty]
        private string _messageText = "";

        [ObservableProperty]
        private ServerModel? _selectedServer; // Rendre nullable

        [ObservableProperty]
        private ChannelModel? _selectedChannel; // Rendre nullable

        [ObservableProperty]
        private bool _isSidebarExpanded = false;

        public void CheckScreenSize(Control control)
        {
            if (control != null)
            {
                var window = control.GetVisualRoot() as Window;
                if (window != null)
                {
                    _window = window;
                    UpdateSidebarState();
                    _window.PropertyChanged += (s, e) =>
                    {
                        if (e.Property == Window.BoundsProperty)
                        {
                            UpdateSidebarState();
                        }
                    };
                }
            }
        }

        private void UpdateSidebarState()
        {
            if (_window != null)
            {
                IsSidebarExpanded = _window.Bounds.Width > COLLAPSE_WIDTH_THRESHOLD;
            }
        }

        [RelayCommand]
        private void ToggleSidebar()
        {
            IsSidebarExpanded = !IsSidebarExpanded;
        }

        public HomeViewModel()
        {
            // Initialisation des données de démonstration
            InitializeDemoData();
        }

        private void InitializeDemoData()
        {
            // Créer quelques serveurs
            Servers = new ObservableCollection<ServerModel>
            {
                new ServerModel { Id = 1, Name = "Général", Icon = "🏠" },
                new ServerModel { Id = 2, Name = "Projets", Icon = "📊" },
                new ServerModel { Id = 3, Name = "Marketing", Icon = "📣" },
                new ServerModel { Id = 4, Name = "Design", Icon = "🎨" }
            };

            // Sélectionner le premier serveur par défaut
            SelectedServer = Servers.FirstOrDefault();

            // Créer quelques canaux
            Channels = new ObservableCollection<ChannelModel>
            {
                new ChannelModel { Id = 1, ServerId = 1, Name = "Général", Icon = "# " },
                new ChannelModel { Id = 2, ServerId = 1, Name = "Annonces", Icon = "# " },
                new ChannelModel { Id = 3, ServerId = 1, Name = "Questions", Icon = "# " }
            };

            // Sélectionner le premier canal par défaut
            SelectedChannel = Channels.FirstOrDefault();

            // Créer quelques messages
            Messages = new ObservableCollection<MessageModel>
            {
                new MessageModel { Id = 1, ChannelId = 1, Username = "System", Content = "Bienvenue sur Connecto!", Timestamp = "Aujourd'hui à 10:00", AvatarUrl = "/Assets/logo.png" },
                new MessageModel { Id = 2, ChannelId = 1, Username = "Admin", Content = "Ceci est une démonstration de l'interface", Timestamp = "Aujourd'hui à 10:05", AvatarUrl = "/Assets/logo.png" },
                new MessageModel { Id = 3, ChannelId = 1, Username = "User1", Content = "Super application !", Timestamp = "Aujourd'hui à 10:10", AvatarUrl = "/Assets/logo.png" }
            };
        }

        [RelayCommand]
        private void SendMessage()
        {
            if (string.IsNullOrWhiteSpace(MessageText) || SelectedChannel == null)
                return;

            Messages.Add(new MessageModel
            {
                Id = Messages.Count + 1,
                ChannelId = SelectedChannel.Id,
                Username = CurrentUser,
                Content = MessageText,
                Timestamp = "À l'instant",
                AvatarUrl = "/Assets/logo.png"
            });

            MessageText = "";
        }

        [RelayCommand]
        private void SelectServer(ServerModel server)
        {
            if (server == null) return;
            
            SelectedServer = server;
            
            // Mettre à jour les canaux en fonction du serveur sélectionné
            var filteredChannels = Channels.Where(c => c.ServerId == server.Id).ToList();
            Channels = new ObservableCollection<ChannelModel>(filteredChannels);
            SelectedChannel = Channels.FirstOrDefault();
        }

        [RelayCommand]
        private void SelectChannel(ChannelModel channel)
        {
            if (channel == null) return;
            
            SelectedChannel = channel;
            
            // Mettre à jour les messages en fonction du canal sélectionné
            var filteredMessages = Messages.Where(m => m.ChannelId == channel.Id).ToList();
            Messages = new ObservableCollection<MessageModel>(filteredMessages);
        }
    }

    // Classes de modèle
    public class ServerModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // Initialisation
        public string Icon { get; set; } = string.Empty; // Initialisation
    }

    public class ChannelModel
    {
        public int Id { get; set; }
        public int ServerId { get; set; }
        public string Name { get; set; } = string.Empty; // Initialisation
        public string Icon { get; set; } = string.Empty; // Initialisation
    }

    public class MessageModel
    {
        public int Id { get; set; }
        public int ChannelId { get; set; }
        public string Username { get; set; } = string.Empty; // Initialisation
        public string Content { get; set; } = string.Empty; // Initialisation
        public string Timestamp { get; set; } = string.Empty; // Initialisation
        public string AvatarUrl { get; set; } = string.Empty; // Initialisation
    }
}