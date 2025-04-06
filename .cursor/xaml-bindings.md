# XAML et Bindings dans Avalonia

## Problèmes Courants et Solutions

### 1. Bindings qui ne fonctionnent pas
**Problème** : Les propriétés ne se mettent pas à jour
**Solution** :
```xaml
<UserControl xmlns="https://github.com/avaloniaui"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:vm="using:ProjectName.ViewModels"
             x:DataType="vm:MyViewModel">
    <TextBlock Text="{Binding PropertyName}" />
</UserControl>
```

### 2. Erreurs de Namespace
**Problème** : Les contrôles personnalisés ne sont pas reconnus
**Solution** : Ajouter les bons namespaces
```xaml
<UserControl xmlns="https://github.com/avaloniaui"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:converters="using:ProjectName.Converters"
             xmlns:controls="using:ProjectName.Controls">
```

### 3. Ressources non trouvées
**Problème** : Les ressources ne sont pas accessibles
**Solution** : Déclarer correctement les ressources
```xaml
<UserControl.Resources>
    <converters:AssetPathConverter x:Key="AssetPathConverter"/>
    <SolidColorBrush x:Key="PrimaryColor" Color="#FF5733"/>
</UserControl.Resources>
```

## Bonnes Pratiques

1. **Déclaration des Contrôles**
   ```xaml
   <Button Content="Click Me"
           Command="{Binding ClickCommand}"
           Classes="primary" />
   ```

2. **Styles et Thèmes**
   ```xaml
   <Style Selector="Button.primary">
       <Setter Property="Background" Value="{DynamicResource PrimaryColor}"/>
       <Setter Property="Transitions">
           <Transitions>
               <BrushTransition Property="Background" Duration="0:0:0.2"/>
           </Transitions>
       </Setter>
   </Style>
   ```

3. **Animations**
   ```xaml
   <Border.Transitions>
       <Transitions>
           <ThicknessTransition Property="Margin" Duration="0:0:0.2"/>
       </Transitions>
   </Border.Transitions>
   ```

## Vérifications

1. Vérifier que le DataContext est correctement défini
2. Vérifier que les propriétés sont bien déclarées dans le ViewModel
3. Vérifier que les namespaces sont corrects
4. Vérifier que les ressources sont correctement déclarées

## Astuces de Débogage

1. Utiliser FallbackValue pour détecter les bindings qui ne fonctionnent pas :
   ```xaml
   <TextBlock Text="{Binding PropertyName, FallbackValue='Not Found'}" />
   ```

2. Activer le débogage des bindings :
   ```xaml
   <TextBlock Text="{Binding PropertyName, Debug=1}" />
   ``` 