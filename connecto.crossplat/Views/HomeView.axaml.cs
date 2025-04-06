using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Interactivity;
using System;
using Avalonia.Platform;
using connecto.crossplat.ViewModels;

namespace connecto.crossplat.Views
{
    public partial class HomeView : UserControl
    {
        private Border? _messageInputArea;
        private TextBox? _messageTextBox;
        private bool _isMobilePlatform;
        private HomeViewModel? _viewModel;

        public HomeView()
        {
            InitializeComponent();
            
            _messageInputArea = this.FindControl<Border>("MessageInputArea");
            _messageTextBox = this.FindControl<TextBox>("MessageTextBox");

            // Détection de la plateforme
            _isMobilePlatform = IsMobilePlatform();

            if (_messageTextBox != null)
            {
                _messageTextBox.GotFocus += MessageTextBox_GotFocus;
                _messageTextBox.LostFocus += MessageTextBox_LostFocus;
            }

            _viewModel = DataContext as HomeViewModel;
        }

        protected override void OnLoaded(RoutedEventArgs e)
        {
            base.OnLoaded(e);
            if (_viewModel != null)
            {
                _viewModel.CheckScreenSize(this);
                _viewModel.SetMessageInputArea(MessageInputArea);
            }
        }

        private bool IsMobilePlatform()
        {
            var os = OperatingSystem.IsAndroid() || OperatingSystem.IsIOS();
            return os;
        }

        private void MessageTextBox_GotFocus(object? sender, GotFocusEventArgs e)
        {
            if (_viewModel != null)
            {
                _viewModel.OnKeyboardVisible();
            }
        }

        private void MessageTextBox_LostFocus(object? sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                _viewModel.OnKeyboardHidden();
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}