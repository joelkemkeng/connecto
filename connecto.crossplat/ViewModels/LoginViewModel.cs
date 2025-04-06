using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace connecto.crossplat.ViewModels
{
    public partial class LoginViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string _username = "";

        [ObservableProperty]
        private string _password = "";

        [ObservableProperty]
        private string _errorMessage = "";

        [ObservableProperty]
        private bool _isLoading = false;

        [ObservableProperty]
        private bool _showPassword = false;

        [ObservableProperty]
        private string _passwordChar = "•";

        // Propriété pour la navigation
        public event EventHandler<NavigationEventArgs>? NavigationRequested;

        [RelayCommand]
        private void TogglePasswordVisibility()
        {
            ShowPassword = !ShowPassword;
            PasswordChar = ShowPassword ? "" : "•";
        }

        [RelayCommand]
        private async Task Login()
        {
            IsLoading = true;
            ErrorMessage = "";

            // Simulons un délai d'authentification
            await Task.Delay(1000);

            // Logique d'authentification simple pour l'exemple
            if (Username == "admin" && Password == "password")
            {
                // Authentification réussie - Naviguer vers la page d'accueil
                NavigationRequested?.Invoke(this, new NavigationEventArgs("Home"));
            }
            else
            {
                ErrorMessage = "Nom d'utilisateur ou mot de passe incorrect";
            }

            IsLoading = false;
        }

        [RelayCommand]
        private void Register()
        {
            // Dans un cas réel, on naviguerait vers la page d'inscription
            ErrorMessage = "Redirection vers la page d'inscription...";
        }
    }

    // Classe pour les événements de navigation
    public class NavigationEventArgs : EventArgs
    {
        public string ViewName { get; }

        public NavigationEventArgs(string viewName)
        {
            ViewName = viewName ?? throw new ArgumentNullException(nameof(viewName));
        }
    }
}