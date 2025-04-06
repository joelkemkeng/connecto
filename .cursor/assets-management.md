# Gestion des Assets dans Avalonia

## Problèmes Courants et Solutions

### 1. Images non chargées
**Problème** : Les images ne s'affichent pas ou sont manquantes
**Solution** :
```xml
<!-- ProjectName.csproj -->
<ItemGroup>
  <AvaloniaResource Include="Assets/**" />
</ItemGroup>
```

### 2. Chemins d'accès incorrects
**Problème** : Les chemins d'accès aux images ne fonctionnent pas
**Solution** : Utiliser le format correct `avares://`
```csharp
public class AssetPathConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string path)
        {
            try
            {
                if (path.StartsWith("/")) path = path.Substring(1);
                var assets = AssetLoader.Open(new Uri($"avares://{AppDomain.CurrentDomain.FriendlyName}/{path}"));
                return new Bitmap(assets);
            }
            catch { return null; }
        }
        return null;
    }
}
```

### 3. Gestion des erreurs
**Problème** : L'application plante lors du chargement des images
**Solution** : Ajouter une gestion d'erreurs robuste
```csharp
try
{
    var asset = AssetLoader.Open(new Uri("avares://AppName/Assets/image.png"));
}
catch (Exception ex)
{
    Debug.WriteLine($"Asset loading error: {ex.Message}");
}
```

## Bonnes Pratiques

1. **Organisation des Assets**
   ```
   Assets/
   ├── Images/
   ├── Fonts/
   └── Styles/
   ```

2. **Déclaration des Ressources**
   ```xaml
   <UserControl.Resources>
       <converters:AssetPathConverter x:Key="AssetPathConverter"/>
   </UserControl.Resources>
   ```

3. **Utilisation dans XAML**
   ```xaml
   <Image Source="{Binding ImagePath, Converter={StaticResource AssetPathConverter}}" />
   ```

## Vérifications

1. Vérifier que les fichiers sont marqués comme "AvaloniaResource"
2. Vérifier que les chemins sont corrects dans le projet
3. Vérifier que le convertisseur est correctement déclaré
4. Vérifier que les erreurs sont correctement gérées

## Commandes Utiles

```bash
# Vérifier les ressources incluses
dotnet msbuild -t:ListAvaloniaResources
``` 