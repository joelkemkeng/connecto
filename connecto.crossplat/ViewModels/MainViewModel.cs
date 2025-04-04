using CommunityToolkit.Mvvm.ComponentModel;

namespace connecto.crossplat.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private ViewModelBase _currentView;

    [ObservableProperty]
    private LoginViewModel _loginVM;

    [ObservableProperty]
    private HomeViewModel _homeVM;

    public MainViewModel()
    {
        // Initialiser les ViewModels
        LoginVM = new LoginViewModel();
        HomeVM = new HomeViewModel();

        // Configurer la navigation
        LoginVM.NavigationRequested += OnNavigationRequested;

        // Définir la vue initiale
        CurrentView = LoginVM;
    }

    private void OnNavigationRequested(object? sender, NavigationEventArgs e)
    {
        if (e.ViewName == "Home")
        {
            CurrentView = HomeVM;
        }
    }
}