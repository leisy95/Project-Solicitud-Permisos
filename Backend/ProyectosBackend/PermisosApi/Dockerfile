# ------------------- Build stage -------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Directorio base dentro del contenedor
WORKDIR /src

# Copiar solo el .csproj primero (para cachear dependencias)
COPY Backend/ProyectosBackend/PermisosApi/PermisosApi.csproj PermisosApi/

# Cambiar al directorio donde está el .csproj
WORKDIR /src/PermisosApi

# Restaurar dependencias
RUN dotnet restore PermisosApi.csproj

# Copiar el resto del código fuente
COPY Backend/ProyectosBackend/PermisosApi/. ./

# Publicar la aplicación
RUN dotnet publish -c Release -o /app/out

# ------------------- Runtime stage -------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app
COPY --from=build /app/out .

EXPOSE 80
ENTRYPOINT ["dotnet", "PermisosApi.dll"]