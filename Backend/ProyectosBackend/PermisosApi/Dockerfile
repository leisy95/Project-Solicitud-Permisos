# ------------------- Build stage -------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar solo el archivo de solución para cachear dependencias
COPY PermisosApi.sln ./

# Copiar la carpeta del proyecto
COPY PermisosApi/ ./PermisosApi/

# Restaurar todas las dependencias usando la solución
RUN dotnet restore PermisosApi.sln

# Construir y publicar la aplicación
RUN dotnet publish PermisosApi/PermisosApi.csproj -c Release -o /app/out

# ------------------- Runtime stage -------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copiar la publicación desde la etapa build
COPY --from=build /app/out .

EXPOSE 80
ENTRYPOINT ["dotnet", "PermisosApi.dll"]