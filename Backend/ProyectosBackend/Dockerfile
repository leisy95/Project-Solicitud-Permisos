# Etapa base
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copiar todo el contenido
COPY . .

# Restaurar dependencias del proyecto específico
RUN dotnet restore PermisosApi/PermisosApi.csproj

# Construir y publicar
RUN dotnet publish PermisosApi/PermisosApi.csproj -c Release -o /app/publish

# Etapa runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Exponer puerto
EXPOSE 8080

# Ejecutar la app
ENTRYPOINT ["dotnet", "PermisosApi.dll"]