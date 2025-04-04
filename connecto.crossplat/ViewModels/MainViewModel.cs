using CommunityToolkit.Mvvm.ComponentModel;
namespace connecto.crossplat.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _greeting = "Welcome to Avalonia!";
    
    public LoginViewModel LoginVM { get; }

    public MainViewModel()
    {
        // Initialiser le LoginViewModel
        LoginVM = new LoginViewModel();
    }
}