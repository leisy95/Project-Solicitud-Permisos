# Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copiar solo el csproj para restaurar dependencias
COPY PermisosApi.csproj ./

# Restaurar dependencias
RUN dotnet restore

# Copiar todo el código
COPY . ./

# Publicar la app
RUN dotnet publish -c Release -o out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/out .
EXPOSE 80
ENTRYPOINT ["dotnet", "PermisosApi.dll"]