FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copier seulement les fichiers nécessaires pour la restauration
#COPY ./connecto.crossplat/*.csproj ./connecto.crossplat/
#COPY ./connecto.crossplat.Browser/*.csproj ./connecto.crossplat.Browser/
#COPY ./connecto.crossplat.sln .


# Copier tout et restaurer comme projets distincts
COPY . .

# Restaurer les dépendances
#RUN dotnet restore connecto.crossplat.Browser
# Publier directement l'application web (ignore les dépendances problématiques)
RUN dotnet publish connecto.crossplat.Browser/connecto.crossplat.Browser.csproj -c Release -o /app/publish /p:SkipRestorePackages=true


# Copier le reste du code
COPY ./connecto.crossplat/. ./connecto.crossplat/
COPY ./connecto.crossplat.Browser/. ./connecto.crossplat.Browser/

# Publier l'application
RUN dotnet publish connecto.crossplat.Browser -c Release -o /app/publish

# Vérifier ce qui a été généré
RUN ls -la /app/publish/wwwroot

# Image finale avec nginx
FROM nginx:alpine
WORKDIR /usr/share/nginx/html

# Copier tout le contenu du dossier publish (pas seulement wwwroot)
COPY --from=build /app/publish .
COPY nginx.conf /etc/nginx/nginx.conf
EXPOSE 80

# Afficher le contenu pour déboguer
RUN ls -la /usr/share/nginx/html






# Publication de l'application 
FROM nginx:alpine
WORKDIR /usr/share/nginx/html
COPY ./publish/wwwroot .
COPY nginx.conf /etc/nginx/nginx.conf
EXPOSE 80