# =========================
# Build stage
# =========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar el archivo de solución y proyecto
COPY PermisosApi.sln ./
COPY PermisosApi/ ./PermisosApi/

# Publicar directamente (restore implícito)
RUN dotnet publish PermisosApi/PermisosApi.csproj -c Release -o /app/out

# =========================
# Runtime stage
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copiar los archivos publicados desde la etapa build
COPY --from=build /app/out ./

# Exponer puerto 80
EXPOSE 80

# Ejecutar la aplicación
ENTRYPOINT ["dotnet", "PermisosApi.dll"]
Guarda y cierra:

CTRL + O → Enter

CTRL + X → salir de nano