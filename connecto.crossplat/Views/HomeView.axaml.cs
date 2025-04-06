using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Interactivity;
using System;
using Avalonia.Platform;

namespace connecto.crossplat.Views
{
    public partial class HomeView : UserControl
    {
        private Border? _messageInputArea;
        private TextBox? _messageTextBox;
        private bool _isMobilePlatform;

        public HomeView()
        {
            InitializeComponent();
            
            _messageInputArea = this.FindControl<Border>("MessageInputArea");
            _messageTextBox = this.FindControl<TextBox>("MessageTextBox");

            // Détection de la plateforme
            _isMobilePlatform = IsMobilePlatform();

            if (_messageTextBox != null)
            {
                _messageTextBox.GotFocus += OnMessageTextBoxGotFocus;
                _messageTextBox.LostFocus += OnMessageTextBoxLostFocus;
            }
        }

        private bool IsMobilePlatform()
        {
            var os = OperatingSystem.IsAndroid() || OperatingSystem.IsIOS();
            return os;
        }

        private void OnMessageTextBoxGotFocus(object? sender, GotFocusEventArgs e)
        {
            if (_messageInputArea != null && _isMobilePlatform)
            {
                _messageInputArea.Margin = new Thickness(0, 0, 0, 300);
            }
        }

        private void OnMessageTextBoxLostFocus(object? sender, RoutedEventArgs e)
        {
            if (_messageInputArea != null && _isMobilePlatform)
            {
                _messageInputArea.Margin = new Thickness(0);
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}