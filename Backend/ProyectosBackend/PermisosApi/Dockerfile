# Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copiar el archivo .csproj (con su ruta correcta)
COPY PermisosApi/PermisosApi.csproj PermisosApi/

# Restaurar dependencias
RUN dotnet restore PermisosApi/PermisosApi.csproj

# Copiar todo el código
COPY . .

# Publicar la aplicación
RUN dotnet publish PermisosApi/PermisosApi.csproj -c Release -o /app/out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/out .
EXPOSE 80
ENTRYPOINT ["dotnet", "PermisosApi.dll"]