# Patterns MVVM dans Avalonia

## Problèmes Courants et Solutions

### 1. ViewModels non réactifs
**Problème** : Les changements de propriétés ne déclenchent pas de mises à jour
**Solution** : Utiliser CommunityToolkit.Mvvm
```csharp
public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _title;

    [RelayCommand]
    private async Task LoadDataAsync()
    {
        // Implementation
    }
}
```

### 2. Commandes non fonctionnelles
**Problème** : Les commandes ne répondent pas aux interactions
**Solution** : Utiliser les attributs RelayCommand
```csharp
[RelayCommand]
private void DoSomething()
{
    // Implementation
}
```

### 3. Communication entre ViewModels
**Problème** : Les ViewModels ne communiquent pas entre eux
**Solution** : Utiliser un service de messagerie
```csharp
public class MessageService
{
    private readonly IMessageBus _messageBus;
    
    public void SendMessage<T>(T message)
    {
        _messageBus.Send(message);
    }
}
```

## Bonnes Pratiques

1. **Structure des ViewModels**
   ```csharp
   public partial class MainViewModel : ViewModelBase
   {
       [ObservableProperty]
       private string _title;

       [ObservableProperty]
       private bool _isLoading;

       [RelayCommand]
       private async Task LoadDataAsync()
       {
           IsLoading = true;
           try
           {
               // Implementation
           }
           finally
           {
               IsLoading = false;
           }
       }
   }
   ```

2. **Validation des Données**
   ```csharp
   public partial class LoginViewModel : ViewModelBase
   {
       [ObservableProperty]
       [Required]
       [EmailAddress]
       private string _email;

       [ObservableProperty]
       [Required]
       [MinLength(6)]
       private string _password;
   }
   ```

3. **Navigation**
   ```csharp
   public class NavigationService
   {
       public void NavigateTo<T>() where T : ViewModelBase
       {
           // Implementation
       }
   }
   ```

## Vérifications

1. Vérifier que les ViewModels héritent de ViewModelBase
2. Vérifier que les propriétés sont marquées avec [ObservableProperty]
3. Vérifier que les commandes sont marquées avec [RelayCommand]
4. Vérifier que les dépendances sont injectées correctement

## Astuces de Débogage

1. Utiliser le débogage des propriétés :
   ```csharp
   [ObservableProperty]
   [NotifyPropertyChangedFor(nameof(FullName))]
   private string _firstName;
   ```

2. Activer les notifications de changement :
   ```csharp
   [ObservableProperty]
   [NotifyPropertyChanged]
   private string _title;
   ``` 