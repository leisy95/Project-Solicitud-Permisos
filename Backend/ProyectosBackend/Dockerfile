# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar la solución y el proyecto
COPY PermisosApi.sln ./
COPY PermisosApi/ ./PermisosApi/

# Restaurar dependencias
RUN dotnet restore PermisosApi/PermisosApi.csproj

# Construir el proyecto
WORKDIR /src/PermisosApi
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish ./

# Exponer puerto
EXPOSE 80
EXPOSE 443

# Comando para iniciar la aplicación
ENTRYPOINT ["dotnet", "PermisosApi.dll"]