# --- Étape 1 : Build et publication de l'application Web ---
    FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
    WORKDIR /app
    
    # Copier l'ensemble du code source
    COPY . .
    
    # Publier directement l'application web Browser
    RUN dotnet publish connecto.crossplat.Browser/connecto.crossplat.Browser.csproj -c Release -o /app/publish /p:SkipRestorePackages=true
    
    # (Optionnel) Vérifier ce qui a été généré dans le dossier wwwroot
    RUN ls -la /app/publish/wwwroot
    
    # --- Étape 2 : Image finale basée sur Nginx ---
    FROM nginx:alpine
    WORKDIR /usr/share/nginx/html
    
    # Copier le contenu publié depuis l'étape de build
    COPY --from=build /app/publish .
    
    # Copier le fichier de configuration nginx (voir ci-dessous)
    COPY nginx.conf /etc/nginx/nginx.conf
    
    EXPOSE 80
    
    # (Optionnel) Afficher le contenu pour déboguer
    RUN ls -la /usr/share/nginx/html
    