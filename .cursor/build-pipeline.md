# Pipeline de Build dans Avalonia

## Problèmes Courants et Solutions

### 1. Assets manquants
**Problème** : Les ressources ne sont pas incluses dans le build
**Solution** : Vérifier la configuration
```xml
<ItemGroup>
  <AvaloniaResource Include="Assets/**" />
  <AvaloniaResource Include="Styles/**" />
</ItemGroup>
```

### 2. Erreurs de Build
**Problème** : Le build échoue sans message clair
**Solution** : Utiliser la séquence complète
```bash
dotnet clean
dotnet restore
dotnet build
```

### 3. Déploiement Mobile
**Problème** : L'application ne se déploie pas correctement
**Solution** : Vérifier les configurations spécifiques
```xml
<PropertyGroup>
  <TargetFramework>net7.0-android</TargetFramework>
  <OutputType>Exe</OutputType>
  <AndroidPackageFormat>aab</AndroidPackageFormat>
</PropertyGroup>
```

## Bonnes Pratiques

1. **Séquence de Build**
   ```bash
   # Nettoyer
   dotnet clean
   
   # Restaurer les packages
   dotnet restore
   
   # Builder
   dotnet build
   
   # Lancer
   dotnet run --project ProjectName.Desktop
   ```

2. **Vérification des Assets**
   ```bash
   # Lister les ressources
   dotnet msbuild -t:ListAvaloniaResources
   
   # Vérifier les dépendances
   dotnet list package
   ```

3. **Configuration Multi-Plateforme**
   ```xml
   <PropertyGroup>
     <TargetFrameworks>net7.0;net7.0-android;net7.0-ios</TargetFrameworks>
     <UseMaui>true</UseMaui>
     <SingleProject>true</SingleProject>
   </PropertyGroup>
   ```

## Vérifications

1. Vérifier que tous les assets sont inclus
2. Vérifier que les dépendances sont à jour
3. Vérifier que les configurations sont correctes
4. Vérifier que les erreurs sont traitées

## Astuces de Débogage

1. Activer les logs détaillés :
   ```bash
   dotnet build -v detailed
   ```

2. Vérifier les ressources incluses :
   ```bash
   dotnet msbuild -t:ListAvaloniaResources -v:detailed
   ```

3. Nettoyer le cache :
   ```bash
   dotnet nuget locals all --clear
   ``` 