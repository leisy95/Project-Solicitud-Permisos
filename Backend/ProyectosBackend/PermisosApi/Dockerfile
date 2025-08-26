# ------------------- Build stage -------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar solo el csproj para cache de dependencias
COPY PermisosApi/*.csproj ./PermisosApi/

# Cambiar WORKDIR al proyecto y restaurar dependencias
WORKDIR /app/PermisosApi
RUN dotnet restore

# Copiar todo el código fuente
COPY PermisosApi/. ./

# Publicar la aplicación
RUN dotnet publish -c Release -o /app/out

# ------------------- Runtime stage -------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .
EXPOSE 80
ENTRYPOINT ["dotnet", "PermisosApi.dll"]