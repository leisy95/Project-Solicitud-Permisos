# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copiar solo el csproj para restaurar dependencias
COPY PermisosApi.csproj ./
RUN dotnet restore

# Copiar todo el código fuente
COPY . ./

# Publicar en Release
RUN dotnet publish -c Release -o out

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/out .
EXPOSE 80
ENTRYPOINT ["dotnet", "PermisosApi.dll"]