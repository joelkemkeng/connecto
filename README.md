# Connecto - Application de Messagerie Cross-Platform

Connecto est une application de messagerie moderne développée avec Avalonia UI, fonctionnant sur Desktop, Web, Android et iOS.

# 🚀 Guide de Déploiement Connecto

## 📋 Prérequis

Avant de commencer, assurez-vous d'avoir installé :

- [Git](https://git-scm.com/downloads)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Visual Studio Code](https://code.visualstudio.com/) (recommandé)

## 📥 Installation

### 1. Cloner le Projet

```bash
# Cloner le repository
git clone https://github.com/joelkemkeng/connecto.git

# Accéder au dossier du projet
cd connecto
```

### 2. Configuration de l'Environnement

```bash
# Installer les templates Avalonia (nécessaire une seule fois)
dotnet new install Avalonia.Templates

# Installer les workloads nécessaires
dotnet workload install wasm-tools
```

### 3. Restauration des Dépendances

```bash
# Restaurer les packages NuGet
dotnet restore connecto.crossplat.sln
```

## 🌐 Déploiement Web avec Docker

### 1. Construction de l'Image

```bash
# Se placer dans le dossier du projet
cd connecto/connecto/connecto.crossplat

# Construire l'image Docker
docker compose build --no-cache
```

### 2. Lancement de l'Application

```bash
# Démarrer l'application
docker compose up
```

L'application sera accessible à l'adresse : http://localhost:8080

### 3. Arrêt de l'Application

```bash
# Pour arrêter l'application (dans un autre terminal)
docker compose down
```

## 💻 Déploiement en Mode Desktop

### Prérequis pour le Mode Desktop

1. **Système d'exploitation compatible** :
   - Windows 10 ou plus récent
   - macOS 10.15 (Catalina) ou plus récent
   - Linux (Ubuntu 20.04, Fedora 35 ou distributions similaires)

2. **Installations requises** :
   - [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
   - [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) ou [Visual Studio Code](https://code.visualstudio.com/)
   - Si Visual Studio Code :
     - Extension C# Dev Kit
     - Extension Avalonia for VS Code

3. **Extensions et Workloads .NET** :
   ```bash
   # Installer les templates Avalonia
   dotnet new install Avalonia.Templates

   # Vérifier l'installation du SDK .NET
   dotnet --version

   # Vérifier les workloads installés
   dotnet workload list
   ```

### Étapes de Configuration

1. **Cloner le Projet** (si pas déjà fait) :
   ```bash
   git clone https://github.com/joelkemkeng/connecto.git
   cd connecto
   ```

2. **Restauration des Packages** :
   ```bash
   # Accéder au dossier du projet
   cd connecto/connecto/connecto.crossplat

   # Restaurer les dépendances
   dotnet restore

   # Nettoyer le projet (en cas de problèmes)
   dotnet clean
   ```

3. **Compilation du Projet** :
   ```bash
   # Compiler en mode Debug
   dotnet build

   # OU compiler en mode Release
   dotnet build -c Release
   ```

### Lancement de l'Application

1. **Mode Développement** :
   ```bash
   # Lancer directement depuis la racine du projet
   dotnet run --project connecto.crossplat.Desktop
   ```

2. **Mode Release** :
   ```bash
   # Compiler en Release
   dotnet publish connecto.crossplat.Desktop -c Release

   # Accéder au dossier de publication
   cd connecto.crossplat.Desktop/bin/Release/net9.0/publish

   # Lancer l'application
   ./connecto.crossplat.Desktop
   ```

### Vérification de l'Installation Desktop

1. **Vérifications Initiales** :
   - L'application se lance sans erreurs
   - La fenêtre principale s'affiche correctement
   - Les polices et icônes sont visibles

2. **Test des Fonctionnalités** :
   - Connexion avec les identifiants par défaut :
     - Utilisateur : admin
     - Mot de passe : password
   - Navigation dans les différentes sections
   - Test de l'envoi/réception de messages

3. **Vérification des Performances** :
   - L'application répond rapidement
   - Les animations sont fluides
   - La mémoire utilisée est raisonnable

### Résolution des Problèmes Desktop Courants

1. **Erreur de SDK manquant** :
   ```bash
   # Vérifier la version du SDK
   dotnet --info

   # Réinstaller le SDK si nécessaire
   # Télécharger depuis https://dotnet.microsoft.com/download/dotnet/9.0
   ```

2. **Problèmes de Compilation** :
   ```bash
   # Nettoyage complet
   dotnet clean
   # Supprimer les dossiers bin et obj
   rm -rf */bin */obj
   # Restaurer et reconstruire
   dotnet restore
   dotnet build
   ```

3. **Erreurs de Dépendances** :
   ```bash
   # Vérifier les packages NuGet
   dotnet list package
   # Mettre à jour tous les packages
   dotnet add package Microsoft.NET.Sdk.WebAssembly --version 9.0.0
   ```

### Configuration Recommandée

- **Matériel Minimal** :
  - Processeur : Double cœur 2 GHz
  - RAM : 4 GB minimum
  - Espace disque : 1 GB libre
  - Carte graphique : Compatible DirectX 10 ou OpenGL 3.3

- **Configuration Optimale** :
  - Processeur : Quad core 2.5 GHz
  - RAM : 8 GB ou plus
  - SSD avec 2 GB d'espace libre
  - Carte graphique dédiée avec 2 GB VRAM

## 🔍 Vérification de l'Installation

### Pour la Version Web
1. Ouvrez http://localhost:8080 dans votre navigateur
2. Vérifiez que la page d'accueil s'affiche
3. Testez la connexion avec les identifiants par défaut

### Pour la Version Desktop
1. Vérifiez que l'application se lance
2. La fenêtre principale devrait s'afficher
3. Testez la connexion avec les identifiants par défaut

## 🛠️ Résolution des Problèmes Courants

### Erreur de Port Déjà Utilisé
```bash
# Trouver le processus utilisant le port 8080
netstat -ano | findstr :8080

# Arrêter le processus (remplacer XXXX par le PID)
taskkill /PID XXXX /F
```

### Problèmes de Build Docker
```bash
# Nettoyer les conteneurs et images
docker compose down
docker system prune -a

# Reconstruire
docker compose build --no-cache
```

### Erreurs de Compilation .NET
```bash
# Nettoyage complet
dotnet clean
rm -rf */bin */obj

# Reconstruction
dotnet restore
dotnet build
```

## 📝 Notes Importantes

- L'application utilise le port 8080 pour la version web
- Docker Desktop doit être en cours d'exécution pour la version web
- Les modifications du code nécessitent une reconstruction de l'image Docker

## 🔐 Identifiants par Défaut

- **Nom d'utilisateur** : admin
- **Mot de passe** : password

## 📚 Structure du Projet

```
connecto/
├── connecto.crossplat/           # Projet principal
│   ├── Models/                  # Modèles de données
│   ├── Views/                   # Vues XAML
│   ├── ViewModels/             # ViewModels
│   └── Converters/             # Convertisseurs
├── connecto.crossplat.Desktop/  # Version Desktop
├── connecto.crossplat.Browser/  # Version Web
├── Dockerfile.web              # Configuration Docker
└── docker-compose.yml          # Configuration Docker Compose
```

## 🤝 Support

Si vous rencontrez des problèmes :
1. Vérifiez les logs Docker : `docker compose logs`
2. Vérifiez l'état des conteneurs : `docker compose ps`
3. Consultez les issues GitHub
4. Ouvrez une nouvelle issue si nécessaire

## 🔄 Mise à Jour du Projet

Pour mettre à jour votre copie locale :

```bash
git pull origin main
docker compose build --no-cache  # Si vous utilisez Docker
```

## 🔗 Liens Utiles
- [Documentation Avalonia](https://docs.avaloniaui.net/)
- [Documentation .NET](https://docs.microsoft.com/fr-fr/dotnet/)
- [Documentation Docker](https://docs.docker.com/)

## 👨‍💻 Guide du Développeur

### Workflow de Développement Desktop

1. **Après Modification du Code** :
   ```bash
   # Arrêter l'application si elle est en cours d'exécution
   # Nettoyer la solution
   dotnet clean

   # Restaurer les packages
   dotnet restore

   # Compiler
   dotnet build

   # Lancer l'application desktop
   dotnet run --project connecto.crossplat.Desktop
   ```

2. **En Cas d'Erreurs de Compilation** :
   ```bash
   # Supprimer les dossiers bin et obj
   rm -rf */bin */obj

   # Nettoyer le cache NuGet local
   dotnet nuget locals all --clear

   # Réinstaller les packages et rebuilder
   dotnet restore
   dotnet build
   ```

3. **Pour le Hot Reload** :
   ```bash
   # Lancer avec le hot reload activé
   dotnet watch run --project connecto.crossplat.Desktop
   ```

## 🌐 Déploiement Web Sans Docker

### 1. Prérequis Web
- .NET SDK 9.0 ou supérieur
- Un navigateur moderne (Chrome, Firefox, Edge)
- Node.js (pour certaines dépendances web)

### 2. Configuration Web
```bash
# Installer les outils WebAssembly
dotnet workload install wasm-tools

# Vérifier l'installation
dotnet workload list
```

### 3. Compilation et Exécution Web
```bash
# Compiler en mode Release
dotnet publish connecto.crossplat.Browser -c Release

# Lancer le serveur de développement
dotnet run --project connecto.crossplat.Browser

# OU pour le développement avec hot reload
dotnet watch run --project connecto.crossplat.Browser
```

### 4. Accès à l'Application Web
- Ouvrir http://localhost:5000 dans votre navigateur
- Pour un accès externe : http://[votre-ip]:5000

## 📱 Déploiement Mobile

### 1. Prérequis Android
- Android Studio installé
- SDK Android (API 33 minimum)
- JDK 17 ou supérieur
- Variables d'environnement configurées :
  ```bash
  JAVA_HOME=chemin/vers/jdk
  ANDROID_HOME=chemin/vers/android/sdk
  ```

### 2. Configuration Android
1. **Activer le Mode Développeur sur l'appareil** :
   - Paramètres → À propos du téléphone
   - Appuyer 7 fois sur "Numéro de build"
   - Retour aux paramètres → Options pour les développeurs
   - Activer "Débogage USB"

2. **Configurer ADB** :
   ```bash
   # Vérifier les appareils connectés
   adb devices

   # Si aucun appareil n'est listé :
   adb kill-server
   adb start-server
   ```

### 3. Compilation et Déploiement Android
```bash
# Installer les workloads nécessaires
dotnet workload install android ios maui-android

# Compiler pour Android
dotnet build -f net9.0-android

# Déployer sur l'appareil
dotnet build -f net9.0-android -t:Install

# Lancer l'application
dotnet build -f net9.0-android -t:Run
```

### 4. Débogage Android
```bash
# Voir les logs en temps réel
adb logcat -s "connecto"

# Nettoyer les données de l'application
adb shell pm clear com.companyname.connecto.crossplat
```

### 5. Configuration iOS (macOS uniquement)
1. **Prérequis** :
   - Mac avec macOS Ventura ou plus récent
   - Xcode 15 ou plus récent
   - Compte développeur Apple

2. **Installation** :
   ```bash
   # Installer les outils iOS
   dotnet workload install ios maui-ios

   # Vérifier l'installation
   dotnet workload list
   ```

3. **Compilation et Déploiement iOS** :
   ```bash
   # Compiler pour iOS
   dotnet build -f net9.0-ios

   # Déployer sur le simulateur
   dotnet build -f net9.0-ios -t:Run
   ```

## 🔍 Vérification des Déploiements

### Desktop
- L'application se lance correctement
- Les animations sont fluides
- La barre latérale se redimensionne
- Les messages s'envoient correctement

### Web
- Le chargement initial est rapide
- Les WebAssembly sont chargés correctement
- La réactivité est bonne
- Les websockets fonctionnent

### Mobile
- L'application s'adapte à la taille de l'écran
- Le clavier virtuel ne cache pas le champ de texte
- Les animations sont fluides
- La consommation de ressources est raisonnable

## ⚠️ Résolution des Problèmes Courants

### Problèmes de Build
```bash
# Erreur de SDK
dotnet --info
dotnet new --install Microsoft.DotNet.Web.ProjectTemplates.9.0

# Erreur de packages
dotnet nuget locals all --clear
dotnet restore --force
```

### Problèmes Android
```bash
# Erreur de SDK Android
$ANDROID_HOME/tools/bin/sdkmanager --update
$ANDROID_HOME/tools/bin/sdkmanager "platform-tools" "platforms;android-33"

# Erreur d'émulateur
$ANDROID_HOME/tools/bin/avdmanager create avd -n test -k "system-images;android-33;google_apis;x86_64"
```

### Problèmes iOS
```bash
# Erreur de certificats
security unlock-keychain
xcode-select --install

# Erreur de provisioning
xcrun simctl list
```