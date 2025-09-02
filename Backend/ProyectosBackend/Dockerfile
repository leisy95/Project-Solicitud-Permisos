# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copiar todo el proyecto
COPY . .

# Restaurar dependencias apuntando al csproj
RUN dotnet restore PermisosApi/PermisosApi.csproj

# Compilar y publicar
RUN dotnet publish PermisosApi/PermisosApi.csproj -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app

# Copiar el output del build
COPY --from=build /app/publish .

# Puerto por defecto
ENV ASPNETCORE_URLS=http://+:8080

# Ejecutar la app
ENTRYPOINT ["dotnet", "PermisosApi.dll"]