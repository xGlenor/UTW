FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ClientUTW/ClientUTW.csproj", "ClientUTW/"]
COPY ["BaseLibrary/BaseLibrary.csproj", "BaseLibrary/"]
RUN dotnet restore "ClientUTW/ClientUTW.csproj"
COPY . .
WORKDIR "/src/ClientUTW"
RUN dotnet build "ClientUTW.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ClientUTW.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

FROM nginx:latest AS final
WORKDIR /usr/share/nginx/html

# Kopiowanie opublikowanych plików aplikacji Blazor do katalogu Nginx
COPY --from=publish /app/publish .

# Kopiowanie niestandardowego pliku konfiguracyjnego Nginx
COPY nginx.conf /etc/nginx/nginx.conf
