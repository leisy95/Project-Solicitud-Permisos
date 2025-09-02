# Etapa base: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Etapa build: SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar solución y proyecto
COPY PermisosApi.sln ./
COPY PermisosApi/ ./PermisosApi/

# Restaurar dependencias
WORKDIR /src/PermisosApi
RUN dotnet restore

# Construir el proyecto
RUN dotnet build -c Release -o /app/build

# Publicar
RUN dotnet publish -c Release -o /app/publish

# Etapa final: runtime
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# Configurar puerto (opcional, se puede sobrescribir con variable)
ENV ASPNETCORE_URLS=http://+:80
ENTRYPOINT ["dotnet", "PermisosApi.dll"]