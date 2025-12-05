# Dockerfile para DeckBrew.Api (Clean Architecture)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiar archivos de proyecto
COPY ["src/DeckBrew.Domain/DeckBrew.Domain.csproj", "DeckBrew.Domain/"]
COPY ["src/DeckBrew.Application/DeckBrew.Application.csproj", "DeckBrew.Application/"]
COPY ["src/DeckBrew.Infrastructure/DeckBrew.Infrastructure.csproj", "DeckBrew.Infrastructure/"]
COPY ["src/DeckBrew.Api/DeckBrew.Api.csproj", "DeckBrew.Api/"]

# Restaurar dependencias
RUN dotnet restore "DeckBrew.Api/DeckBrew.Api.csproj"

# Copiar código fuente
COPY src/ .

# Build
WORKDIR "/src/DeckBrew.Api"
RUN dotnet build "DeckBrew.Api.csproj" -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish "DeckBrew.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 8100
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DeckBrew.Api.dll"]
