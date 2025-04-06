# 🐳 Règles Cursor pour la Dockerisation d'Applications Avalonia Web

## 1. 📁 Structure des Fichiers Docker

### 1.1 Organisation des Fichiers
```plaintext
/
├── Dockerfile.web              # Configuration Docker pour le web
├── docker-compose.yml         # Configuration des services
├── .dockerignore             # Fichiers à ignorer
└── connecto.crossplat/
    └── connecto.crossplat.Browser/
        └── Properties/
            └── launchSettings.json  # Configuration du lancement
```

### 1.2 Contenu Minimal du .dockerignore
```plaintext
**/bin/
**/obj/
**/.vs/
**/.vscode/
**/node_modules/
```

## 2. 🔧 Configuration Docker

### 2.1 Dockerfile.web - Structure Standard
```dockerfile
# Étape 1: SDK pour la compilation
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copier les fichiers de projet
COPY ["connecto.crossplat/connecto.crossplat.csproj", "connecto.crossplat/"]
COPY ["connecto.crossplat.Browser/connecto.crossplat.Browser.csproj", "connecto.crossplat.Browser/"]

# Restaurer les dépendances
RUN dotnet restore "connecto.crossplat.Browser/connecto.crossplat.Browser.csproj"

# Copier le reste du code
COPY . .

# Installer les workloads nécessaires
RUN dotnet workload install wasm-tools

# Publier l'application
RUN dotnet publish "connecto.crossplat.Browser/connecto.crossplat.Browser.csproj" -c Release -o /app/publish

# Étape 2: Runtime pour l'exécution
FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=build /app/publish/wwwroot .
COPY nginx.conf /etc/nginx/nginx.conf
```

### 2.2 docker-compose.yml - Configuration Standard
```yaml
version: '3.8'
services:
  web:
    build:
      context: .
      dockerfile: Dockerfile.web
    ports:
      - "8080:80"
    environment:
      - ASPNETCORE_ENVIRONMENT=Production
```

## 3. ⚠️ Points de Vigilance

### 3.1 Erreurs Courantes
- ❌ Ne pas oublier d'installer `wasm-tools`
- ❌ Ne pas oublier de copier le `nginx.conf`
- ❌ Ne pas oublier de configurer les ports correctement
- ❌ Ne pas oublier de gérer les variables d'environnement

### 3.2 Vérifications Avant Build
```bash
# 1. Vérifier l'installation des workloads
dotnet workload list

# 2. Vérifier la présence des fichiers essentiels
ls Dockerfile.web docker-compose.yml nginx.conf

# 3. Vérifier la configuration nginx
cat nginx.conf
```

## 4. 🚀 Commandes de Build et Déploiement

### 4.1 Commandes de Base
```bash
# Nettoyer les conteneurs existants
docker compose down

# Construire l'image
docker compose build --no-cache

# Démarrer l'application
docker compose up -d

# Vérifier les logs
docker compose logs -f
```

### 4.2 Commandes de Dépannage
```bash
# Vérifier les conteneurs en cours
docker ps

# Inspecter les logs du conteneur
docker logs [container_id]

# Entrer dans le conteneur
docker exec -it [container_id] sh
```

## 5. 🔍 Vérification du Déploiement

### 5.1 Liste de Contrôle
- [ ] L'application est accessible sur http://localhost:8080
- [ ] Les ressources statiques sont correctement servies
- [ ] Les logs ne montrent pas d'erreurs
- [ ] La performance est acceptable

### 5.2 Tests de Base
```bash
# Vérifier l'accessibilité
curl -I http://localhost:8080

# Vérifier les ressources statiques
curl -I http://localhost:8080/css/app.css
curl -I http://localhost:8080/js/app.js
```

## 6. 📝 Bonnes Pratiques

### 6.1 Sécurité
- Utiliser des images officielles et à jour
- Ne pas exposer de ports inutiles
- Utiliser des variables d'environnement pour les configurations sensibles
- Minimiser la taille de l'image finale

### 6.2 Performance
- Optimiser les couches Docker
- Utiliser le multi-stage building
- Minimiser le nombre de RUN commands
- Nettoyer les caches de build

## 7. 🔄 Mise à Jour et Maintenance

### 7.1 Procédure de Mise à Jour
```bash
# 1. Arrêter les conteneurs
docker compose down

# 2. Mettre à jour le code source
git pull

# 3. Reconstruire les images
docker compose build --no-cache

# 4. Redémarrer les conteneurs
docker compose up -d
```

### 7.2 Maintenance
- Vérifier régulièrement les mises à jour des images de base
- Nettoyer les images et conteneurs inutilisés
- Surveiller l'utilisation des ressources
- Maintenir une documentation à jour

## 8. 📚 Ressources Utiles

- [Documentation Officielle Avalonia](https://docs.avaloniaui.net/)
- [Documentation Docker](https://docs.docker.com/)
- [Guide Nginx pour .NET](https://docs.microsoft.com/aspnet/core/host-and-deploy/linux-nginx) 