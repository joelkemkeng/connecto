using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                // Authentification réussie
                ErrorMessage = ""; // On efface tout message d'erreur
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
}