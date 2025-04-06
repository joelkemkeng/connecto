# Adaptations Mobile dans Avalonia

## Problèmes Courants et Solutions

### 1. Détection de Plateforme
**Problème** : Comment adapter l'interface selon la plateforme
**Solution** : Utiliser les APIs de détection
```csharp
private bool IsMobilePlatform()
{
    return OperatingSystem.IsAndroid() || OperatingSystem.IsIOS();
}
```

### 2. Gestion du Clavier
**Problème** : Le clavier virtuel cache le contenu
**Solution** : Ajuster la mise en page
```csharp
private void OnKeyboardVisible()
{
    if (_isMobilePlatform && _messageInputArea != null)
    {
        _messageInputArea.Margin = new Thickness(0, 0, 0, 300);
    }
}
```

### 3. Tailles d'Écran
**Problème** : L'interface ne s'adapte pas aux différentes tailles
**Solution** : Utiliser des layouts responsifs
```xaml
<Grid>
    <Grid.RowDefinitions>
        <RowDefinition Height="Auto"/>
        <RowDefinition Height="*"/>
    </Grid.RowDefinitions>
    <Grid.ColumnDefinitions>
        <ColumnDefinition Width="*"/>
        <ColumnDefinition Width="Auto"/>
    </Grid.ColumnDefinitions>
</Grid>
```

## Bonnes Pratiques

1. **Layouts Responsifs**
   ```xaml
   <Grid>
       <Grid.Styles>
           <Style Selector="Grid > *">
               <Style.Triggers>
                   <DataTrigger Binding="{Binding IsMobile}" Value="True">
                       <Setter Property="Grid.ColumnSpan" Value="2"/>
                   </DataTrigger>
               </Style.Triggers>
           </Style>
       </Grid.Styles>
   </Grid>
   ```

2. **Tailles Adaptatives**
   ```xaml
   <Style Selector="Button">
       <Setter Property="FontSize">
           <Setter.Value>
               <Binding Path="IsMobile" Converter="{StaticResource FontSizeConverter}"/>
           </Setter.Value>
       </Setter>
   </Style>
   ```

3. **Gestes Tactiles**
   ```xaml
   <Button>
       <Button.Gestures>
           <TapGestureRecognizer Tapped="OnTapped"/>
       </Button.Gestures>
   </Button>
   ```

## Vérifications

1. Vérifier que la détection de plateforme fonctionne
2. Vérifier que les layouts s'adaptent correctement
3. Vérifier que le clavier est géré correctement
4. Vérifier que les gestes tactiles fonctionnent

## Astuces de Débogage

1. Simuler différentes tailles d'écran :
   ```csharp
   public void SimulateScreenSize(double width, double height)
   {
       // Implementation
   }
   ```

2. Tester les gestes tactiles :
   ```csharp
   public void TestTouchGesture()
   {
       // Implementation
   }
   ``` 