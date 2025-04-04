using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;
using System.Reactive;

namespace connecto.crossplat.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _username = "";
        private string _password = "";
        private string _errorMessage = "";
        private bool _isLoading = false;

        public string Username
        {
            get => _username;
            set => this.RaiseAndSetIfChanged(ref _username, value);
        }

        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => this.RaiseAndSetIfChanged(ref _errorMessage, value);
        }

        public bool IsLoading
        {
            get => _isLoading;
            set => this.RaiseAndSetIfChanged(ref _isLoading, value);
        }

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = ReactiveCommand.CreateFromTask(ExecuteLoginAsync);
            RegisterCommand = ReactiveCommand.Create(ExecuteRegister);
        }

        private async Task ExecuteLoginAsync()
        {
            IsLoading = true;
            ErrorMessage = "";

            // Simulons un délai d'authentification
            await Task.Delay(1000);

            // Logique d'authentification simple pour l'exemple
            if (Username == "admin" && Password == "password")
            {
                // Authentification réussie - Dans un cas réel, on naviguerait vers le dashboard
                // ou on stockerait un token d'authentification
                ErrorMessage = ""; // On efface tout message d'erreur
            }
            else
            {
                ErrorMessage = "Nom d'utilisateur ou mot de passe incorrect";
            }

            IsLoading = false;
        }

        private void ExecuteRegister()
        {
            // Dans un cas réel, on naviguerait vers la page d'inscription
            ErrorMessage = "Redirection vers la page d'inscription...";
        }
    }
}