# Guide de déploiement pour Connecto

Ce document explique comment installer, configurer et déployer l'application Connecto en mode desktop et web.

## Prérequis

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- Visual Studio 2022, Visual Studio Code, ou JetBrains Rider
- Git
- Accès à un terminal/ligne de commande

## Installation et configuration

### 1. Cloner le projet

```bash
git clone https://github.com/your-username/connecto.git
cd connecto
```

### 2. Installer les templates Avalonia

```bash
dotnet new install Avalonia.Templates
```

### 3. Restaurer les dépendances

```bash
dotnet restore connecto.crossplat.sln
```

### 4. Installer les charges de travail .NET (optionnel, pour les plateformes mobiles)

Si vous souhaitez également travailler sur les versions Android ou iOS :

```bash
# Pour Android
dotnet workload install android

# Pour iOS (nécessite un Mac)
dotnet workload install ios
```

## Structure du projet

Le projet Connecto est une application cross-platform construite avec Avalonia UI :

- `connecto.crossplat` - Projet principal contenant la logique de l'application
- `connecto.crossplat.Desktop` - Configuration spécifique pour la version desktop
- `connecto.crossplat.Browser` - Configuration spécifique pour la version web
- `connecto.crossplat.Android` - Configuration spécifique pour Android (optionnel)
- `connecto.crossplat.iOS` - Configuration spécifique pour iOS (optionnel)

## Exécution de l'application

### Version Desktop

```bash
dotnet run --project connecto.crossplat.Desktop
```

### Version Web

```bash
dotnet run --project connecto.crossplat.Browser
```

Si le port est déjà utilisé, vous pouvez modifier le port dans `connecto.crossplat.Browser/Properties/launchSettings.json`.

## Dépendances principales

| Dépendance | Version | Description |
|------------|---------|-------------|
| Avalonia | 11.2.6 | Framework UI cross-platform |
| Avalonia.Desktop | 11.2.6 | Support pour applications desktop |
| Avalonia.Browser | 11.2.6 | Support pour applications web |
| Avalonia.Themes.Fluent | 11.2.6 | Thème Fluent pour Avalonia |
| Avalonia.Fonts.Inter | 11.2.6 | Polices Inter pour Avalonia |
| CommunityToolkit.Mvvm | 8.2.1 | Toolkit MVVM pour .NET |

## Débogage

### Visual Studio / Visual Studio Code

1. Ouvrez la solution `connecto.crossplat.sln`
2. Sélectionnez le projet de démarrage (Desktop ou Browser)
3. Appuyez sur F5 pour démarrer le débogage

### Ligne de commande

```bash
# Pour la version desktop
dotnet run --project connecto.crossplat.Desktop

# Pour la version web
dotnet run --project connecto.crossplat.Browser
```

## Développement

### Ajouter un nouveau écran

1. Créez une nouvelle classe ViewModel dans le dossier `ViewModels`
2. Créez une nouvelle vue XAML dans le dossier `Views`
3. Ajoutez la navigation vers cette vue dans `MainViewModel.cs`

### Authentification

Les identifiants de démonstration sont :
- Nom d'utilisateur : `admin`
- Mot de passe : `password`

## Déploiement Docker

Voici comment conteneuriser l'application Connecto :

1. Créez un fichier `Dockerfile` à la racine du projet :

```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copier les fichiers de projet et restaurer les dépendances
COPY *.sln .
COPY connecto.crossplat/*.csproj ./connecto.crossplat/
COPY connecto.crossplat.Browser/*.csproj ./connecto.crossplat.Browser/
COPY connecto.crossplat.Desktop/*.csproj ./connecto.crossplat.Desktop/
RUN dotnet restore

# Copier tout le code source et construire
COPY . .
RUN dotnet build -c Release --no-restore

# Publier l'application
FROM build AS publish
RUN dotnet publish connecto.crossplat.Browser -c Release -o /app/publish --no-build

# Créer l'image finale
FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY nginx.conf /etc/nginx/nginx.conf
EXPOSE 80
```

2. Créez un fichier `nginx.conf` pour configurer le serveur web :

```
events { }
http {
    include mime.types;
    types {
        application/wasm wasm;
    }

    server {
        listen 80;
        
        location / {
            root /usr/share/nginx/html;
            try_files $uri $uri/ /index.html =404;
        }
    }
}
```

3. Construisez et exécutez l'image Docker :

```bash
# Construire l'image
docker build -t connecto:latest .

# Exécuter le conteneur
docker run -d -p 8080:80 connecto:latest
```

4. Accédez à l'application à l'adresse http://localhost:8080

## Résolution des problèmes courants

### Port déjà utilisé

Si vous rencontrez l'erreur "Failed to bind to address ... address already in use", exécutez :

```bash
# Trouver le processus qui utilise le port (ex: 7169)
netstat -ano | findstr :7169

# Tuer le processus (remplacez XXXX par le PID trouvé)
taskkill /PID XXXX /F
```

### Problèmes de compilation

Si vous rencontrez des erreurs de compilation :

1. Nettoyez la solution : `dotnet clean`
2. Supprimez les dossiers bin et obj
3. Restaurez les dépendances : `dotnet restore`
4. Recompilez : `dotnet build`

## Ressources supplémentaires

- [Documentation Avalonia](https://docs.avaloniaui.net/)
- [Documentation CommunityToolkit.Mvvm](https://learn.microsoft.com/dotnet/communitytoolkit/mvvm/)
- [Documentation .NET 9](https://learn.microsoft.com/dotnet/core/whats-new/dotnet-9)

Ce guide devrait permettre à n'importe quel développeur de configurer, exécuter et travailler sur le projet Connecto.