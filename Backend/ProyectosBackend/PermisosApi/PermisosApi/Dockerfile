# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copiar los archivos de proyecto primero para restaurar dependencias
COPY *.csproj ./
RUN dotnet restore

# Copiar todo el código y publicar en Release
COPY . ./
RUN dotnet publish -c Release -o out

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app

# Copiar la salida de la etapa de build
COPY --from=build /app/out .

# Exponer puerto 80
EXPOSE 80

# Entrypoint
ENTRYPOINT ["dotnet", "PermisosApi.dll"]