# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar solo el csproj primero
COPY PermisosApi/*.csproj ./PermisosApi/

# Restaurar dependencias
WORKDIR /app/PermisosApi
RUN dotnet restore

# Copiar todo el código
COPY PermisosApi/. ./

# Publicar
RUN dotnet publish -c Release -o /app/out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .
EXPOSE 80
ENTRYPOINT ["dotnet", "PermisosApi.dll"]