using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.VisualTree;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using connecto.crossplat.Models;
using connecto.crossplat.Services;

namespace connecto.crossplat.ViewModels
{
    public partial class HomeViewModel : ViewModelBase
    {
        private const double COLLAPSE_WIDTH_THRESHOLD = 1000;
        private Window? _window;
        private bool _isMobilePlatform;
        private Border? _messageInputArea;
        private readonly DataService _dataService;

        [ObservableProperty]
        private bool _isSidebarExpanded = true;

        [ObservableProperty]
        private string _messageText = string.Empty;

        [ObservableProperty]
        private string _currentUser = "User";

        [ObservableProperty]
        private Server? _selectedServer;

        [ObservableProperty]
        private Channel? _selectedChannel;

        [ObservableProperty]
        private ObservableCollection<Server> _servers = new();

        [ObservableProperty]
        private ObservableCollection<Channel> _channels = new();

        [ObservableProperty]
        private ObservableCollection<Message> _messages = new();

        public HomeViewModel()
        {
            _dataService = new DataService();
            
            // Initialiser les collections depuis le service
            Servers = _dataService.Servers;
            SelectedServer = Servers.FirstOrDefault();
            
            UpdateChannels();

            _isMobilePlatform = OperatingSystem.IsAndroid() || OperatingSystem.IsIOS();
        }

        public void SetMessageInputArea(Border messageInputArea)
        {
            _messageInputArea = messageInputArea;
        }

        partial void OnSelectedServerChanged(Server? value)
        {
            if (value != null)
            {
                UpdateChannels();
            }
        }

        partial void OnSelectedChannelChanged(Channel? value)
        {
            if (value != null)
            {
                UpdateMessages();
            }
        }

        private void UpdateChannels()
        {
            if (SelectedServer != null)
            {
                Channels = _dataService.GetChannelsForServer(SelectedServer.Id);
                SelectedChannel = Channels.FirstOrDefault();
            }
        }

        private void UpdateMessages()
        {
            if (SelectedChannel != null)
            {
                Messages = _dataService.GetMessagesForChannel(SelectedChannel.Id);
            }
        }

        [RelayCommand]
        private void ToggleSidebar()
        {
            IsSidebarExpanded = !IsSidebarExpanded;
        }

        [RelayCommand]
        private void SendMessage()
        {
            if (string.IsNullOrWhiteSpace(MessageText) || SelectedChannel == null)
                return;

            _dataService.AddMessage(SelectedChannel.Id, CurrentUser, MessageText);
            UpdateMessages();
            MessageText = "";
        }

        public void CheckScreenSize(Control control)
        {
            _window = control.GetVisualRoot() as Window;
            if (_window == null) return;

            UpdateSidebarState(_window.Bounds.Width);
            _window.PropertyChanged += (s, e) =>
            {
                if (e.Property == Window.BoundsProperty)
                {
                    UpdateSidebarState(_window.Bounds.Width);
                }
            };
        }

        private void UpdateSidebarState(double width)
        {
            IsSidebarExpanded = width > COLLAPSE_WIDTH_THRESHOLD;
        }

        public void OnKeyboardVisible()
        {
            if (_isMobilePlatform && _messageInputArea != null)
            {
                _messageInputArea.Margin = new Thickness(0, 0, 0, 300);
            }
        }

        public void OnKeyboardHidden()
        {
            if (_isMobilePlatform && _messageInputArea != null)
            {
                _messageInputArea.Margin = new Thickness(0);
            }
        }
    }
}