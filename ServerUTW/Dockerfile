FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ServerUTW/ServerUTW.csproj", "ServerUTW/"]
COPY ["BaseLibrary/BaseLibrary.csproj", "BaseLibrary/"]
RUN dotnet restore "ServerUTW/ServerUTW.csproj"
COPY . .
WORKDIR "/src/ServerUTW"
RUN dotnet build "ServerUTW.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ServerUTW.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "ServerUTW.dll"]
