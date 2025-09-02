# Base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar la solución y el proyecto al contenedor
COPY PermisosApi/PermisosApi.sln ./
COPY PermisosApi/PermisosApi/PermisosApi.csproj PermisosApi/

# Restaurar dependencias
RUN dotnet restore

# Copiar todo el código
COPY PermisosApi/. ./PermisosApi/

# Construir y publicar
WORKDIR /src/PermisosApi
RUN dotnet build -c Release -o /app/build
RUN dotnet publish -c Release -o /app/publish

# Etapa final
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "PermisosApi.dll"]